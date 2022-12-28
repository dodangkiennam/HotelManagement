using HotelManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers
{
    public class ErrorController: Controller
    {
        [HttpGet]
        [Route("/Error/404")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
