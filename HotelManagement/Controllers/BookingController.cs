using HotelManagement.Data;
using HotelManagement.Models;
using HotelManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;

namespace HotelManagement.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BookingController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public List<RoomType> GetAvailableRoomTypes(DateTime CheckIn, DateTime CheckOut, int MaxAdult, int MaxChild)
        {
            var bookingsNotInRange = from b in _context.Bookings
                                     where (DateTime.Compare(b.CheckIn, CheckOut)>0 || DateTime.Compare(b.CheckOut, CheckIn)<0)
                                     select b;

            var calculatedRoomTypes = from rt in _context.RoomTypes.Include(c => c.RoomTypeImages)
                                     where (rt.MaxAdult >= MaxAdult && rt.MaxChild >= MaxChild)
                                     select new RoomType()
                                     {
                                         RoomTypeId = rt.RoomTypeId,
                                         Name = rt.Name,
                                         Price = rt.Price,
                                         MaxAdult = rt.MaxAdult,
                                         MaxChild = rt.MaxChild,
                                         Description = rt.Description,
                                         Quantity = rt.Quantity - (from b in _context.Bookings
                                                                   where (b.RoomTypeId==rt.RoomTypeId && b.Status!="CANCEL"
                                                                   && !bookingsNotInRange.Any(bnir => bnir.BookingId == b.BookingId))
                                                                   select b).Count(),
                                         RoomTypeImages = rt.RoomTypeImages
                                     };

            var availableRoomTypes = from rt in calculatedRoomTypes
                                     where rt.Quantity > 0
                                     select rt;

            return availableRoomTypes.ToList();
        }

        public IActionResult Index()
        {
            BookingSearchViewModel model = new BookingSearchViewModel()
            {
                CheckIn=DateTime.Now.AddDays(1),
                CheckOut=DateTime.Now.AddDays(2),
                MaxAdult=1,
                MaxChild=0
            };

            var availableRoomTypes = GetAvailableRoomTypes(model.CheckIn, model.CheckOut, model.MaxAdult, model.MaxChild);

            foreach (var rt in availableRoomTypes)
            {
                Console.WriteLine(rt.RoomTypeId + " -- " + rt.Quantity);
            }

            ViewBag.availableRoomTypes = availableRoomTypes;

            return View(model);
        }

        public IActionResult Search(BookingSearchViewModel model)
        {
            var availableRoomTypes = GetAvailableRoomTypes(model.CheckIn, model.CheckOut, model.MaxAdult, model.MaxChild);

            foreach (var rt in availableRoomTypes)
            {
                Console.WriteLine(rt.RoomTypeId + " -- " + rt.Quantity);
            }

            ViewBag.availableRoomTypes = availableRoomTypes;

            return View("Index", model);
        }

        public async Task<IActionResult> Create(DateTime CheckIn, DateTime CheckOut, int RoomTypeId)
        {
            var accId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            var user = await _context.Customers
                .Include(c => c.Account)
                .FirstOrDefaultAsync(c => c.Account.AccId == Convert.ToInt32(accId));

            if(user is null)
            {
                return LocalRedirect("/Account/Login");
            }

            var roomType = await _context.RoomTypes.FirstOrDefaultAsync(o => o.RoomTypeId==RoomTypeId);

            if(roomType is null)
            {
                return LocalRedirect("/Booking");
            }

            double totalDays = Math.Ceiling((CheckOut - CheckIn).TotalDays);
            double totalPrice = totalDays * roomType.Price;

            Booking booking = new Booking()
            {
                CheckIn=CheckIn,
                CheckOut=CheckOut,
                RoomTypeId=RoomTypeId,
                CusId=user.CusId,
                RoomAmount=1,
                CreateDate=DateTime.Now,
                Status="REQUEST_BOOKING",
                TotalPrice=totalPrice
            };

            ViewBag.Customer = user;
            ViewBag.RoomType = roomType;
            
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmedCreate(Booking model)
        {
            if (!ModelState.IsValid)
            {
                return LocalRedirect("/Booking");
            }

            await _context.Bookings.AddAsync(model);
            await _context.SaveChangesAsync();

            return LocalRedirect("/Booking/History");
        }

        public async Task<IActionResult> History()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect("/Account/Login");
            }

            var accId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            var user = await _context.Customers
                .Include(c => c.Account)
                .FirstOrDefaultAsync(c => c.Account.AccId == Convert.ToInt32(accId));

            if(user is null)
            {
                return LocalRedirect("/Account/Login");
            }

            var bookings = await _context.Bookings
                .Include(b => b.RoomType)
                .Include(b => b.Employee)
                .Where(b => b.CusId == user.CusId).ToListAsync();

            return View(bookings);
        }

        [Route("/Booking/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect("/Account/Login");
            }

            var accId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            var user = await _context.Customers
                .Include(c => c.Account)
                .FirstOrDefaultAsync(c => c.Account.AccId == Convert.ToInt32(accId));

            if (user is null)
            {
                return LocalRedirect("/Account/Login");
            }

            var booking = await _context.Bookings
                .Include(b => b.RoomType)
                .Include(b => b.Employee)
                .Include(b => b.Customer)
                .Include(b => b.FeedBack)
                .Where(b => b.BookingId == id).FirstOrDefaultAsync();

            if(booking is null || booking.CusId != user.CusId)
            {
                return LocalRedirect("/Booking/History");
            }

            var bookingServices = _context.BookingServices.Include(b => b.Service).Where(b => b.BookingId == id).ToList();
            var feedbackImages = _context.FeedbackImages.Where(f => f.FeedbackId == booking.FeedbackId).ToList();
            var totalForServices = bookingServices.Sum(b => b.Service.Price);

            ViewBag.ListOfBookingServices = bookingServices;
            ViewBag.TotalForServices = totalForServices;
            ViewBag.TotalPrice = totalForServices + booking.TotalPrice;
            ViewBag.feedbackImages = feedbackImages;

            return View(booking);
        }

        public async Task<IActionResult> Cancel(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect("/Account/Login");
            }

            var accId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            var user = await _context.Customers
                .Include(c => c.Account)
                .FirstOrDefaultAsync(c => c.Account.AccId == Convert.ToInt32(accId));

            if (user is null)
            {
                return LocalRedirect("/Account/Login");
            }

            var booking = await _context.Bookings
                .Where(b => b.BookingId == id).FirstOrDefaultAsync();

            if (booking is null || booking.CusId != user.CusId)
            {
                return LocalRedirect("/Booking/History");
            }

            booking.Status = "CANCEL";
            await _context.SaveChangesAsync();

            return LocalRedirect("/Booking/Details/" + booking.BookingId);
        }

        public async Task<IActionResult> SendFeedback(int BookingId, string Content, double Rating, List<IFormFile> Images)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect("/Account/Login");
            }

            var accId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            var user = await _context.Customers
                .Include(c => c.Account)
                .FirstOrDefaultAsync(c => c.Account.AccId == Convert.ToInt32(accId));

            if (user is null)
            {
                return LocalRedirect("/Account/Login");
            }

            var booking = await _context.Bookings
                .Include(b => b.FeedBack)
                .Where(b => b.BookingId == BookingId).FirstOrDefaultAsync();

            if (booking is null || booking.CusId != user.CusId)
            {
                return LocalRedirect("/Booking/History");
            }

            FeedBack fb = new FeedBack()
            {
                CreateDate=DateTime.Now,
                Content=Content,
                Rating=Rating
            };

            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (Images is not null)
            {
                Console.WriteLine("Uploading " + Images.Count + " images");

                fb.FeedbackImages = new List<FeedbackImage>();

                foreach (var Image in Images)
                {
                    FeedbackImage img = new FeedbackImage();

                    string fileName = Image.FileName;
                    string extension = Path.GetExtension(Image.FileName);

                    img.ImageUrl = fileName = DateTime.Now.ToString("yymmssfff") + extension;

                    fb.FeedbackImages.Add(img);

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await Image.CopyToAsync(fileStream);
                    }
                }

            }

            booking.FeedBack = fb;
            booking.FeedbackId = fb.FeedbackId;
            await _context.SaveChangesAsync();

            return LocalRedirect("/Booking/Details/" + booking.BookingId);
        }
    }
}
