using HotelManagement.Data;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers.Manage
{
    [Route("Manage/[controller]/[action]")]
    public class RoomTypeManageController : Controller
    {
        private readonly AppDbContext _context;

        public RoomTypeManageController(AppDbContext context)
        {
            _context = context;
        }

        private static string GetViewPath(string viewName)
        {
            return $"~/Views/Manage/RoomTypeManage/{viewName}.cshtml";
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
