using HotelManagement.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers.Manage
{
    [Route("Manage/[controller]/[action]")]
    public class EmployeeManageController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeManageController(AppDbContext context)
        {
            _context = context;
        }

        private static string GetViewPath(string viewName)
        {
            return $"~/Views/Manage/EmployeeManage/{viewName}.cshtml";
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
