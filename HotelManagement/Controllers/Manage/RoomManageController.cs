using HotelManagement.Data;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace HotelManagement.Controllers.Manage
{
    [Route("Manage/Room/[action]")]
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

        [HttpGet]
        [Route("/Manage/Room")]
        public async Task<IActionResult> Index()
        {
            var list = await _context.Rooms.Include(o => o.RoomType).ToListAsync();

            return View(GetViewPath("Index"), list);
        }

        [HttpGet]
        [Route("/Manage/Room/Create")]
        public async Task<IActionResult> Create()
        {
            var roomTypes = await _context.RoomTypes.ToListAsync();

            ViewBag.roomTypes = roomTypes;

            Room model = new Room();
            
            return View(GetViewPath("Create"), model);
        }

        [HttpPost]
        [Route("/Manage/Room/Create")]
        public async Task<IActionResult> Create(Room model)
        {
            if (!ModelState.IsValid)
            {
                return View(GetViewPath("Create"), model);
            }

            await _context.Rooms.AddAsync(model);
            await _context.SaveChangesAsync();

            return LocalRedirect("/Manage/Room/Create");
        }

        [HttpPost]
        [Route("Manage/Room/Delete")]
        public async Task<IActionResult> Delete(string roomNo)
        {
            Console.WriteLine(roomNo);
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.RoomNo==roomNo);

            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }

            return LocalRedirect("/Manage/Room");
        }
    }
}
