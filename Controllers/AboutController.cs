using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPJ_ProyectoMVC.Data;
using SPJ_ProyectoMVC.Models;

namespace SPJ_ProyectoMVC.Controllers
{
    public class AboutController : Controller
    {
        private readonly SPJ_ProyectoMVCContext _context;

        public AboutController(SPJ_ProyectoMVCContext context)
        {
            _context = context;
        }

        // GET: About
        public async Task<IActionResult> Index()
        {
            return View(await _context.About.ToListAsync());
        }

        // GET: About/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _context.About
                .FirstOrDefaultAsync(m => m.AboutId == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        private bool AboutExists(int id)
        {
            return _context.About.Any(e => e.AboutId == id);
        }
    }
}
