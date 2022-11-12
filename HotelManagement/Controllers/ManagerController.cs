using HotelManagement.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers
{
    [Authorize("ManagerOnly")]
    public class ManagerController : Controller
    {
        private readonly AppDbContext _context;

        public ManagerController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> HotelStatistic()
        {
            return View();
        }

        public async Task<IActionResult> CustomerManagement()
        {
            var customers = await _context.Customers.Include(x => x.Account).ToListAsync();
            return View(customers);
        }

        public async Task<IActionResult> EmployeeManagement()
        {
            return View();
        }
    }
}
