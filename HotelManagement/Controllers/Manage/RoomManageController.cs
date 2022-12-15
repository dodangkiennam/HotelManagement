using HotelManagement.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers.Manage
{
    [Route("Manage/[controller]/[action]")]
    public class RoomManageController : Controller
    {
        private readonly AppDbContext _context;

        public RoomManageController(AppDbContext context)
        {
            _context = context;
        }

        private static string GetViewPath(string viewName)
        {
            return $"~/Views/Manage/RoomManage/{viewName}.cshtml";
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
