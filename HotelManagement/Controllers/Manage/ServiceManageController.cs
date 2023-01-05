using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Data;
using HotelManagement.Models;
using HotelManagement.ViewModels;
using Microsoft.Extensions.Hosting;

namespace HotelManagement.Controllers.Manage
{
    public class ServiceManageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ServiceManageController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        private static string GetViewPath(string viewName)
        {
            return $"~/Views/Manage/ServiceManage/{viewName}.cshtml";
        }

        public async Task<IActionResult> Index()
        {
              return View(GetViewPath("Index"), await _context.Services.ToListAsync());
        }

        public IActionResult Create()
        {
            return View(GetViewPath("Create"));
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = model.Service.Name;
                string extension = Path.GetExtension(model.ImageFile.FileName);
                model.Service.ImageUrl = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                string path = Path.Combine(wwwRootPath + "/images/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }

                //Insert record
                await _context.Services.AddAsync(model.Service);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(GetViewPath("Create"), model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(GetViewPath("Edit"), service);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, Service service)
        {
            if (id != service.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.ServiceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(GetViewPath("Edit"), service);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Services == null)
            {
                return Problem("Entity set 'AppDbContext.Services'  is null.");
            }
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);

                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", service.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);

                _context.Services.Remove(service);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
          return _context.Services.Any(e => e.ServiceId == id);
        }
    }
}
