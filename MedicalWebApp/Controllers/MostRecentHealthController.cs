using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace MedicalWebApp.Controllers
{
    public class MostRecentHealthController : Controller
    {
        private readonly WebApplication3Context _context;

        public MostRecentHealthController(WebApplication3Context context)
        {
            _context = context;
        }

        // GET: MostRecentHealth
        public async Task<IActionResult> Index(int? minWeight, int? maxWeight, int? minAge, int? maxAge, bool? isSmoker)
        {
            IQueryable<MostRecentHealthInfo> query = _context.MostRecentHealthInfo;

            if (query == null)
            {
                return Problem("Entity set 'WebApplication3Context.MostRecentHealthInfo' is null.");
            }

            if (minWeight.HasValue)
            {
                query = query.Where(info => info.weight_lbs >= minWeight.Value);
            }

            if (maxWeight.HasValue)
            {
                query = query.Where(info => info.weight_lbs <= maxWeight.Value);
            }

            // Apply age filters if provided
            if (minAge.HasValue)
            {
                query = query.Where(info => info.Age >= minAge.Value);
            }

            if (maxAge.HasValue)
            {
                query = query.Where(info => info.Age <= maxAge.Value);
            }

            if (isSmoker.HasValue)
            {
                query = query.Where(info => info.smoke_or_vape == isSmoker.Value);
            }

            var healthInfoList = await query.ToListAsync();

            return View(healthInfoList);
        }


        private bool MostRecentHealthInfoExists(int id)
        {
          return (_context.MostRecentHealthInfo?.Any(e => e.appointment_id == id)).GetValueOrDefault();
        }
    }
}
