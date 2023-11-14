using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using med_test8.Data;
using med_test8.Models;

namespace med_test8.Controllers
{
    public class Our_ProvidersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Our_ProvidersController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string officeLocation, string classification)
        {
            IQueryable<med_test8.Models.Our_Providers> providers = _context.Our_Providers;

            providers = providers
                .Where(p =>
                    (string.IsNullOrEmpty(officeLocation) || p.Office == officeLocation) &&
                    (string.IsNullOrEmpty(classification) || p.classification == classification)
                );

            return View(await providers.ToListAsync());
        }


        // GET: Our_Providers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Our_Providers == null)
            {
                return NotFound();
            }

            var our_Providers = await _context.Our_Providers
                .FirstOrDefaultAsync(m => m.id == id);
            if (our_Providers == null)
            {
                return NotFound();
            }

            return View(our_Providers);
        }

        // GET: Our_Providers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Our_Providers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,Office,classification")] Our_Providers our_Providers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(our_Providers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(our_Providers);
        }

    }
}

