using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using med_test8.Data;
using med_test8.Models;
using System.Security.Claims;

namespace MedicalWebApp.Controllers
{
    public class ReferralsDoctorViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReferralsDoctorViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReferralsDoctorView
        public async Task<IActionResult> Index()
        {
              return _context.Referrals != null ? 
                          View(await _context.ReferralView.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Referrals'  is null.");
        }

        // GET: ReferralsDoctorView/Details/5
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

        // GET: ReferralsDoctorView/Create
        public IActionResult Create()
        {
            var loggedInUserEmail = User.FindFirstValue(ClaimTypes.Email);

            // Retrieve the doctor whose email matches the currently logged-in user's email
            var loggedInDoctor = _context.Doctors
                .FirstOrDefault(d => d.email == loggedInUserEmail);

            if (loggedInDoctor != null)
            {
                // Only include the currently logged-in doctor in the dropdown
                ViewBag.Doctors = new List<object>
            {
                new { Id = loggedInDoctor.doctor_id, FullName = $"{loggedInDoctor.last_name}, {loggedInDoctor.first_name}   {loggedInDoctor.doctor_id}" }
            };
            }
            else
            {
                // Handle the case where the currently logged-in user is not a doctor
                ViewBag.Doctors = new List<object>();
            }

            ViewBag.DoctorSpecialties = _context.DoctorSpecialties
                .OrderBy(d => d.last_name)
                .ThenBy(d => d.first_name)
                .Select(d => new { Id = d.doctor_id, FullName = $"{d.last_name}, {d.first_name} ID{d.doctor_id}  [ {d.classification} ]  " })
                .ToList();

            ViewBag.Patients = _context.Patients
                .OrderBy(d => d.last_name)
                .ThenBy(d => d.first_name)
                .Select(d => new { Id = d.patient_id, FullName = $"{d.last_name}, {d.first_name}   {d.patient_id}" })
                .ToList();

            return View(new Referrals());
        }

        // POST: ReferralsDoctorView/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("referral_id,primary_doctor_id,specialist_doctor_id,speciality_id,referral_date,patient_id")] Referrals referrals)
        {
            if (ModelState.IsValid)
            {
                _context.Add(referrals);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(referrals);
        }

    // GET: ReferralsDoctorView/Edit/5
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

        // POST: ReferralsDoctorView/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("referral_id,specialist_doctor_id,speciality_id,referral_date,patient_id")] Referrals referrals)
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

        // GET: ReferralsDoctorView/Delete/5
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

        // POST: ReferralsDoctorView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Referrals == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Referrals'  is null.");
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

        public JsonResult GetSpecialtyId(int specialistDoctorId)
        {
            var specialtyId = _context.DoctorSpecialties
                .Where(ds => ds.doctor_id == specialistDoctorId)
                .Select(ds => ds.speciality_id)
                .FirstOrDefault();

            return Json(specialtyId);
        }
    }
}
