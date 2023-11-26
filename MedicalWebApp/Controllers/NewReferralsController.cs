using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalWebApp.Models;

namespace MedicalWebApp.Controllers
{
    public class NewReferralsController : Controller
    {
        private readonly team12MainContext _context;

        public NewReferralsController(team12MainContext context)
        {
            _context = context;
        }

        // GET: NewReferrals
        public async Task<IActionResult> Index()
        {
              return _context.ReferralView != null ? 
                          View(await _context.ReferralView.ToListAsync()) :
                          Problem("Entity set 'team12MainContext.ReferralView'  is null.");
        }

        // GET: NewReferrals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReferralView == null)
            {
                return NotFound();
            }

            var referralView = await _context.ReferralView
                .FirstOrDefaultAsync(m => m.referral_id == id);
            if (referralView == null)
            {
                return NotFound();
            }

            return View(referralView);
        }

        // GET: NewReferrals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewReferrals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("referral_id,primary_doctor_name,specialist_doctor_name,patient_name,speciality_classification,referral_date")] ReferralView referralView)
        {
            if (ModelState.IsValid)
            {
                _context.Add(referralView);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(referralView);
        }

        // GET: NewReferrals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReferralView == null)
            {
                return NotFound();
            }

            var referralView = await _context.ReferralView.FindAsync(id);
            if (referralView == null)
            {
                return NotFound();
            }
            return View(referralView);
        }

        // POST: NewReferrals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("referral_id,primary_doctor_name,specialist_doctor_name,patient_name,speciality_classification,referral_date")] ReferralView referralView)
        {
            if (id != referralView.referral_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(referralView);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReferralViewExists(referralView.referral_id))
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
            return View(referralView);
        }

        // GET: NewReferrals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReferralView == null)
            {
                return NotFound();
            }

            var referralView = await _context.ReferralView
                .FirstOrDefaultAsync(m => m.referral_id == id);
            if (referralView == null)
            {
                return NotFound();
            }

            return View(referralView);
        }

        // POST: NewReferrals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReferralView == null)
            {
                return Problem("Entity set 'team12MainContext.ReferralView'  is null.");
            }
            var referralView = await _context.ReferralView.FindAsync(id);
            if (referralView != null)
            {
                _context.ReferralView.Remove(referralView);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReferralViewExists(int id)
        {
          return (_context.ReferralView?.Any(e => e.referral_id == id)).GetValueOrDefault();
        }
    }
}
