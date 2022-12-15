using HotelManagement.Data;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers.Manage
{
    public class CustomerManageController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerManageController(AppDbContext context)
        {
            _context = context;
        }

        private static string GetViewPath(string viewName)
        {
            return $"~/Views/Manage/CustomerManage/{viewName}.cshtml";
        }

        [HttpGet]
        [Route("Manage/Customer/")]
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.Include(x => x.Account).ToListAsync();

            return View(GetViewPath("Index"), customers);
        }
    }
}
