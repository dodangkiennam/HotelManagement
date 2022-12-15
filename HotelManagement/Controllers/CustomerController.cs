using HotelManagement.Data;
using HotelManagement.ViewModels;
using HotelManagement.Models;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers
{
    [Route("[controller]/[action]")]
    public class CustomerController : Controller
    {
        public readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Profile()
        {
            var accId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            var user = _context.Customers
                .Include(c => c.Account)
                .FirstOrDefault(c => c.Account.AccId == Convert.ToInt32(accId));

            if(user == null)
                return LocalRedirect("/");

            return View(user);
        }
    }
}
