using HotelManagement.Data;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers.Manage
{
    [Route("Manage/[controller]/[action]")]
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

        public IActionResult Index()
        {
            return View();
        }


    }
}
