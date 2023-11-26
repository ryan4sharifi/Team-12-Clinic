using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class NewReferralsController : Controller
    {
        private readonly WebApplication3Context _context;

        public NewReferralsController(WebApplication3Context context)
        {
            _context = context;
        }

        // GET: Referrals
        public async Task<IActionResult> Index()
        {
              return _context.Referrals != null ? 
                          View(await _context.Referrals.ToListAsync()) :
                          Problem("Entity set 'WebApplication3Context.Referrals'  is null.");
        }

        // GET: Referrals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Referrals == null)
            {
                return NotFound();
            }

            var referrals = await _context.Referrals
                .FirstOrDefaultAsync(m => m.referral_id == id);
            if (referrals == null)
            {
                return NotFound();
            }

            return View(referrals);
        }

        // GET: Referrals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Referrals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("referral_id,primary_doctor_id,specialist_doctor_id,speciality_id,referral_date,patient_id")] NewReferrals referrals)
        {
            if (ModelState.IsValid)
            {
                _context.Add(referrals);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(referrals);
        }

        // GET: Referrals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Referrals == null)
            {
                return NotFound();
            }

            var referrals = await _context.Referrals.FindAsync(id);
            if (referrals == null)
            {
                return NotFound();
            }
            return View(referrals);
        }

        // POST: Referrals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("referral_id,primary_doctor_id,specialist_doctor_id,speciality_id,referral_date,patient_id")] NewReferrals referrals)
        {
            if (id != referrals.referral_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(referrals);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReferralsExists(referrals.referral_id))
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
            return View(referrals);
        }

        // GET: Referrals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Referrals == null)
            {
                return NotFound();
            }

            var referrals = await _context.Referrals
                .FirstOrDefaultAsync(m => m.referral_id == id);
            if (referrals == null)
            {
                return NotFound();
            }

            return View(referrals);
        }

        // POST: Referrals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Referrals == null)
            {
                return Problem("Entity set 'WebApplication3Context.Referrals'  is null.");
            }
            var referrals = await _context.Referrals.FindAsync(id);
            if (referrals != null)
            {
                _context.Referrals.Remove(referrals);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReferralsExists(int id)
        {
          return (_context.Referrals?.Any(e => e.referral_id == id)).GetValueOrDefault();
        }
    }
}
