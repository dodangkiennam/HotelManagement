using HotelManagement.Data;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace HotelManagement.Controllers.Manage
{
    [Route("Manage/Booking/[action]")]
    public class BookingManageController : Controller
    {
        private readonly AppDbContext _context;

        public BookingManageController(AppDbContext context)
        {
            _context = context;
        }

        private static string GetViewPath(string viewName)
        {
            return $"~/Views/Manage/BookingManage/{viewName}.cshtml";
        }

        [Route("/Manage/Booking")]
        public async Task<IActionResult> Index(string Status="")
        {
            var bookingList = await _context.Bookings
                            .Include(b => b.RoomType)
                            .Include(b => b.Employee)
                            .Include(b => b.Customer)
                            .ToListAsync();

            if(Status != "")
            {
                bookingList = bookingList.Where(b => b.Status == Status).ToList();
            }

            return View(GetViewPath("Index"), bookingList);
        }

        [Route("/Manage/Booking/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.RoomType)
                .Include(b => b.Employee)
                .Include(b => b.Customer)
                .Where(b => b.BookingId == id).FirstOrDefaultAsync();

            if(booking is null)
            {
                return LocalRedirect("/Booking/History");
            }

            var bookingServices =  _context.BookingServices.Include(b => b.Service).Where(b => b.BookingId == id).ToList();
            var totalForServices = bookingServices.Sum(b => b.Service.Price);

            ViewBag.ListOfBookingServices = bookingServices;
            ViewBag.TotalPrice = totalForServices + booking.TotalPrice;

            return View(GetViewPath("Details"), booking);
        }

        public List<Room> GetAvailableRooms(DateTime CheckIn, DateTime CheckOut)
        {
            var bookingsNotInRange = from b in _context.Bookings
                                     where (DateTime.Compare(b.CheckIn, CheckOut) > 0 || DateTime.Compare(b.CheckOut, CheckIn) < 0)
                                     select b;

            var bookingsInRange = from b in _context.Bookings
                                  where b.Status!="CANCEL" && !bookingsNotInRange.Any(bnir => bnir.BookingId == b.BookingId)
                                  select b;

            var availableRooms = from r in _context.Rooms
                                 where !bookingsInRange.Any(b => b.RoomNo == r.RoomNo)
                                 select r;

            return availableRooms.ToList();
        }


        public async Task<IActionResult> Cancel(int id)
        {
            var booking = await _context.Bookings
                       .Where(b => b.BookingId == id)
                       .FirstOrDefaultAsync();

            if (booking is null || booking.Status == "CANCEL")
            {
                return LocalRedirect("/Manage/Booking");
            }

            booking.Status = "CANCEL";

            await _context.SaveChangesAsync();

            return LocalRedirect("/Manage/Booking/Details/" + booking.BookingId);
        }


        public async Task<IActionResult> Confirm(int id)
        {
            var booking = await _context.Bookings
           .Where(b => b.BookingId == id)
           .FirstOrDefaultAsync();

            if (booking is null || booking.Status == "CANCEL")
            {
                return LocalRedirect("/Manage/Booking");
            }

            booking.Status = "BOOKING_SUCCESS";

            await _context.SaveChangesAsync();

            return LocalRedirect("/Manage/Booking/Details/" + booking.BookingId);
        }

        [Route("/Manage/Booking/ConfirmAndSelectRoom/{id}")]
        public async Task<IActionResult> CheckIn(int id)
        {
            var booking = await _context.Bookings
                            .Include(b => b.RoomType)
                            .Include(b => b.Employee)
                            .Include(b => b.Customer)
                            .Where(b => b.BookingId == id)
                            .FirstOrDefaultAsync();

            if (booking is null || booking.RoomType is null)
            {
                return LocalRedirect("/Manage/Booking");
            }

            var availableRooms = GetAvailableRooms(booking.CheckIn, booking.CheckOut);

            var recommendedRooms = from r in availableRooms where r.RoomTypeId == booking.RoomTypeId select r;
            var anotherRooms = from r in availableRooms where r.RoomTypeId != booking.RoomTypeId select r;
            var anotherRoomTypeKeys = from r in anotherRooms group r by r.RoomTypeId into g select new { RoomTypeId = g.Key };
            var anotherRoomTypes = from r in anotherRoomTypeKeys
                                   join rt in _context.RoomTypes on r.RoomTypeId equals rt.RoomTypeId
                                   select rt;

            ViewBag.recommendedRooms = recommendedRooms;
            ViewBag.recommendedRoomType = booking.RoomType;
            ViewBag.anotherRooms = anotherRooms;
            ViewBag.anotherRoomTypes = anotherRoomTypes;

            return View(GetViewPath("ConfirmAndSelectRoom"), booking);
        }

        public async Task<IActionResult> SelectedRoom(int BookingId, string RoomNo)
        {
            var booking = await _context.Bookings
                        .Where(b => b.BookingId == BookingId)
                        .FirstOrDefaultAsync();

            if (booking is null)
            {
                return LocalRedirect("/Manage/Booking");
            }

            if (!booking.RoomNo.IsNullOrEmpty())
            {
                return LocalRedirect("/Manage/Booking");
            }

            booking.RoomNo = RoomNo;
            booking.Status = "CHECKED_IN";
            await _context.SaveChangesAsync();

            return LocalRedirect("/Manage/Booking/Details/" + booking.BookingId);
        }

        public async Task<IActionResult> CheckOut(int id)
        {
            var booking = await _context.Bookings
           .Where(b => b.BookingId == id)
           .FirstOrDefaultAsync();

            if (booking is null || booking.Status == "CANCEL")
            {
                return LocalRedirect("/Manage/Booking");
            }

            booking.Status = "CHECKED_OUT";

            await _context.SaveChangesAsync();

            return LocalRedirect("/Manage/Booking/Details/" + booking.BookingId);
        }

        public async Task<IActionResult> AddService(int id)
        {
            var services = await _context.Services.ToListAsync();

            ViewBag.ListOfServices = services;
            ViewBag.BookingId = id;

            return View(GetViewPath("AddService"));
        }

        [HttpPost]
        public async Task<IActionResult> AddService(int BookingId, List<BookingService> BookingServices)
        {
            var booking = await _context.Bookings
                .Include(b => b.BookingServices)
           .Where(b => b.BookingId == BookingId)
           .FirstOrDefaultAsync();

            if (booking is null || booking.Status == "CANCEL")
            {
                return LocalRedirect("/Manage/Booking");
            }

            foreach (var a in BookingServices)
            {
                a.OrderTime = DateTime.Now;
                booking.TotalPrice = booking.TotalPrice;
            }

            if (booking.BookingServices == null)
            {
                booking.BookingServices = new List<BookingService>();
            }

            booking.BookingServices.AddRange(BookingServices);
            await _context.SaveChangesAsync();

            return LocalRedirect("/Manage/Booking/Details/" + BookingId);
        }

        public async Task<IActionResult> DeleteService(DateTime OrderTime, int BookingId, int ServiceId)
        {
            var booking = await _context.Bookings
                .Include(b => b.BookingServices)
                .Where(b => b.BookingId == BookingId)
                .FirstOrDefaultAsync();

            if (booking is null || booking.BookingServices is null || booking.Status != "CHECKED_IN")
            {
                return LocalRedirect("/Manage/Booking");
            }
 
            foreach(var o in _context.BookingServices)
            {
                if(((DateTime)o.OrderTime).ToString() == OrderTime.ToString() && o.BookingId == BookingId && o.ServiceId == ServiceId){
                    booking.BookingServices.Remove(o);
                }
            }

            await _context.SaveChangesAsync();

            return LocalRedirect("/Manage/Booking/Details/" + BookingId);
        }

        [Route("/Manage/Booking/PrintInvoice/{id}")]
        public async Task<IActionResult> PrintInvoice(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.BookingServices)
                .Where(b => b.BookingId == id)
                .FirstOrDefaultAsync();

            await _context.SaveChangesAsync();

            using var PDF = new IronPdf.ChromePdfRenderer().RenderUrlAsPdf("https://localhost:7116/Manage/Booking/CreateInvoice/" + id);
            return File(PDF.BinaryData, "application/pdf", "Invoice.pdf");
        }

        [Route("/Manage/Booking/CreateInvoice/{id}")]
        public async Task<IActionResult> CreateInvoice(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.RoomType)
                .Include(b => b.Employee)
                .Include(b => b.Customer)
                .Where(b => b.BookingId == id).FirstOrDefaultAsync();

            if (booking is null)
            {
                return LocalRedirect("/Booking/History");
            }

            var bookingServices = _context.BookingServices.Include(b => b.Service).Where(b => b.BookingId == id).ToList();

            var a = from b in bookingServices
                    group b by b.ServiceId into g
                    select new
                    {
                        ServiceId = g.Key,
                        TotalQuantity = g.Sum(o => o.Amount)
                    };

            var a2 = from o in a
                    join o2 in _context.Services on o.ServiceId equals o2.ServiceId
                    select new
                    {
                        Service = o2,
                        TotalAmount = o.TotalQuantity,
                        Sum = o.TotalQuantity * o2.Price
                    };

            var totalForServices = a2.Sum(o => o.Sum);

            ViewBag.BookingServices = a2;
            ViewBag.TotalForServices = totalForServices;
            ViewBag.TotalPrice = totalForServices + booking.TotalPrice;

            return View(GetViewPath("CreateInvoice"), booking);
        }
    }
}
