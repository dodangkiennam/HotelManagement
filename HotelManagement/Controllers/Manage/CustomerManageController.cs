using HotelManagement.Data;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers.Manage
{
    [Route("Manage/Customer/{action}")]
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
        [Route("/Manage/Customer/")]
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.Include(x => x.Account).ToListAsync();

            return View(GetViewPath("Index"), customers);
        }

        [Route("/Manage/Customer/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(e => e.Account)
                .FirstOrDefaultAsync(m => m.CusId == id);

            if (customer == null)
            {
                return NotFound();
            }

            var bookings = _context.Bookings.Include(b => b.RoomType).Where(b => b.CusId == customer.CusId).ToList();

            ViewBag.bookings = bookings;

            return View(GetViewPath("Details"), customer);
        }

        [Route("/Manage/Customer/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'AppDbContext.Employees'  is null.");
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return LocalRedirect("/Manage/Customer");
        }
    }
}
