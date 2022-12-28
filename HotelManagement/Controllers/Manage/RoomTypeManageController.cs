using HotelManagement.Data;
using HotelManagement.Models;
using HotelManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Dynamic;

namespace HotelManagement.Controllers.Manage
{
    [Route("Manage/RoomType/[action]")]
    public class RoomTypeManageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RoomTypeManageController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        private static string GetViewPath(string viewName)
        {
            return $"~/Views/Manage/RoomTypeManage/{viewName}.cshtml";
        }

        [HttpGet]
        [Route("/Manage/RoomType")]
        public async Task<IActionResult> Index()
        {
            var list = await _context.RoomTypes.ToListAsync();

            return View(GetViewPath("Index"), list);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var listOfFacs = await _context.Facilities.ToListAsync();

            ViewBag.ListOfFacs = listOfFacs;

            CreateRoomTypeViewModel model = new CreateRoomTypeViewModel();

            return View(GetViewPath("Create"), model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoomTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(GetViewPath("Create"), model);
            }

            if (model.RoomType is null)
            {
                ViewBag.Message = "Room Type Information is null";
                return View(GetViewPath("Create"), model);
            }

            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (model.Images is not null)
            {
                Console.WriteLine("Uploading " + model.Images.Count + " images");
                
                model.RoomType.RoomTypeImages = new List<RoomTypeImage>();

                foreach (var Image in model.Images)
                {
                    RoomTypeImage rtImage = new RoomTypeImage();

                    string fileName = Image.ImageName;
                    string extension = Path.GetExtension(Image.ImageFile.FileName);

                    rtImage.ImageName = fileName;
                    rtImage.ImageUrl = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    model.RoomType.RoomTypeImages.Add(rtImage);

                    string path = Path.Combine(wwwRootPath + "/images/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await Image.ImageFile.CopyToAsync(fileStream);
                    }
                }

            }

            if(model.FacilityApplies is not null)
            {
                Console.WriteLine("Adding " + model.FacilityApplies.Count + " facilities for this Room Type");

                model.RoomType.FacilityApplies = model.FacilityApplies;
            }

            await _context.RoomTypes.AddAsync(model.RoomType);
            await _context.SaveChangesAsync();

            return LocalRedirect("/Manage/RoomType/Create");
        }

        [HttpGet]
        [Route("/Manage/RoomType/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var roomType = await _context.RoomTypes.FirstOrDefaultAsync(o => o.RoomTypeId==id);

            if (roomType is null)
            {
                return LocalRedirect("/Error/404");
            }

            var roomTypeImages = await _context.RoomTypeImages.Where(o => o.RoomTypeId == id).ToListAsync();
            var facilities = await _context.FacilityApplies
                .Where(o => o.RoomTypeId == id)
                .Join(_context.Facilities,
                    fa => fa.FacId,
                    f => f.FacId,
                    (fa, f) => new {
                        f.Name,
                        f.ImageUrl,
                        fa.Amount
                    })
                .ToListAsync();

            dynamic model = new ExpandoObject();
            model.roomType = roomType;
            model.roomTypeImages = roomTypeImages;
            model.facilities = facilities;

            return View(GetViewPath("Detail"), model);
        }

        [HttpPost]
        [Route("/Manage/RoomType/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var roomType = await _context.RoomTypes.FirstOrDefaultAsync(o => o.RoomTypeId == id);

            if (roomType is null)
            {
                return LocalRedirect("/Error/404");
            }

            var roomTypeImages = await _context.RoomTypeImages.Where(o => o.RoomTypeId == id).ToListAsync();

            if(roomTypeImages is not null && roomTypeImages.Count > 0)
            {
                foreach(var rti in roomTypeImages)
                {
                    var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", rti.ImageUrl);
                    if (System.IO.File.Exists(imagePath))
                        System.IO.File.Delete(imagePath);
                }

                _context.RoomTypeImages.RemoveRange(roomTypeImages);
            }

            var facilityApplies = await _context.FacilityApplies.Where(o => o.RoomTypeId == id).ToListAsync();

            if(facilityApplies is not null && facilityApplies.Count > 0)
            {
                _context.FacilityApplies.RemoveRange(facilityApplies);
            }

            _context.RoomTypes.Remove(roomType);

            await _context.SaveChangesAsync();

            return LocalRedirect("/Manage/RoomType");
        }
    }
}
