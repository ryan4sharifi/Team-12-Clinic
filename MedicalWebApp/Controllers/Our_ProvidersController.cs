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
            if (id == null || _context.Provider_Info == null)
            {
                return NotFound();
            }

            var Provider_Info = await _context.Provider_Info
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Provider_Info == null)
            {
                return NotFound();
            }

            return View(Provider_Info);
        }

  

    

    }
}

