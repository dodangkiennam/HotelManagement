using HotelManagement.Data;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Create()
        {
            Employee model = new Employee()
            {
                Account = new Account()
                {
                    RoleName = "employee",
                    CreateDate = DateTime.Now
                }
            };

            return View(GetViewPath("Create"), model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
            }
            else
            {
                return View(GetViewPath("Create"), employee);
            }

            return LocalRedirect("/Manage/Employee");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(GetViewPath("Edit"), employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Update(employee);
               await _context.SaveChangesAsync();
            }

            return View(GetViewPath("Edit"), employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'AppDbContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return LocalRedirect("/Manage/Employee");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Account)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            var bookings = _context.Bookings
                .Include(b => b.RoomType)
                .Include(b => b.Customer)
                .Where(b => b.EmpId == id).ToList();

            ViewBag.bookings = bookings;

            return View(GetViewPath("Details"), employee);
        }
    }
}
