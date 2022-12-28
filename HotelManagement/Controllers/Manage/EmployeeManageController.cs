using HotelManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers.Manage
{
    [Route("Manage/Employee/[action]")]
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

        [HttpGet]
        [Route("/Manage/Employee")]
        public async Task<IActionResult> Index()
        {
            var list = await _context.Employees.ToListAsync();

            return View(GetViewPath("Index"), list);
        }
    }
}
