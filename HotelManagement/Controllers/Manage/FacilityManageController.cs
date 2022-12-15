using HotelManagement.Data;
using HotelManagement.Models;
using HotelManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;

namespace HotelManagement.Controllers.Manage
{
    [Route("Manage/Facility/[action]")]
    public class FacilityManageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FacilityManageController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        private static string GetViewPath(string viewName)
        {
            return $"~/Views/Manage/FacilityManage/{viewName}.cshtml";
        }

        [HttpGet]
        [Route("/Manage/Facility/")]
        public async Task<IActionResult> Index()
        {
            var facility_list = await _context.Facilities.ToListAsync();
            return View(GetViewPath("Index"), facility_list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateFacilityViewModel model = new CreateFacilityViewModel()
            {
                Facility = new Facility()
                {
                    ImageUrl = "image_name"
                }
            };
            return View(GetViewPath("Create"), model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFacilityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(GetViewPath("Create"), model);
            }

            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = model.Facility.Name;
            string extension = Path.GetExtension(model.ImageFile.FileName);
            model.Facility.ImageUrl = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            
            string path = Path.Combine(wwwRootPath + "/images/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            //Insert record
            _context.Facilities.Add(model.Facility);
            await _context.SaveChangesAsync();

            return LocalRedirect("/Manage/Facility/Create");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var facModel = await _context.Facilities.FindAsync(id);
            Console.WriteLine(id);

            if (facModel != null)
            {
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", facModel.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);

                _context.Facilities.Remove(facModel);
                await _context.SaveChangesAsync();
            }
                

            return LocalRedirect("/Manage/Facility/");
        }
    }
}
