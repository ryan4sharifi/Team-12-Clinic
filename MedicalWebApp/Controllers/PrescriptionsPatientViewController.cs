using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrialRun.Data;
using TrialRun.Models;

namespace MedicalWebApp.Controllers
{
    public class PrescriptionsPatientViewController : Controller
    {
        private readonly TrialRunContext _context;

        public PrescriptionsPatientViewController(TrialRunContext context)
        {
            _context = context;
        }

        // GET: PrescriptionsPatientView
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Get the currently logged-in user's email
                var loggedInUserEmail = User.FindFirstValue(ClaimTypes.Email);

                // Retrieve appointments only for the logged-in patient based on email
                var prescriptions = await _context.DoctorPrescriptions
                    .Where(pa => pa.patient_email == loggedInUserEmail)
                    .ToListAsync();

                return View(prescriptions);
            }

            // Handle the case where the user is not authenticated
            return Challenge();
        }

        // GET: PrescriptionsPatientView/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DoctorPrescriptions == null)
            {
                return NotFound();
            }

            var doctorPrescriptions = await _context.DoctorPrescriptions
                .FirstOrDefaultAsync(m => m.prescription_id == id);
            if (doctorPrescriptions == null)
            {
                return NotFound();
            }

            return View(doctorPrescriptions);
        }

        // GET: PrescriptionsPatientView/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PrescriptionsPatientView/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("prescription_id,doctor_id,patient_id,PatientName,drug_name,dosage,refills,date_prescription,doctor_email")] DoctorPrescriptions doctorPrescriptions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctorPrescriptions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctorPrescriptions);
        }

        // GET: PrescriptionsPatientView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DoctorPrescriptions == null)
            {
                return NotFound();
            }

            var doctorPrescriptions = await _context.DoctorPrescriptions.FindAsync(id);
            if (doctorPrescriptions == null)
            {
                return NotFound();
            }
            return View(doctorPrescriptions);
        }

        // POST: PrescriptionsPatientView/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("prescription_id,doctor_id,patient_id,PatientName,drug_name,dosage,refills,date_prescription,doctor_email")] DoctorPrescriptions doctorPrescriptions)
        {
            if (id != doctorPrescriptions.prescription_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctorPrescriptions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorPrescriptionsExists(doctorPrescriptions.prescription_id))
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
            return View(doctorPrescriptions);
        }

        // GET: PrescriptionsPatientView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DoctorPrescriptions == null)
            {
                return NotFound();
            }

            var doctorPrescriptions = await _context.DoctorPrescriptions
                .FirstOrDefaultAsync(m => m.prescription_id == id);
            if (doctorPrescriptions == null)
            {
                return NotFound();
            }

            return View(doctorPrescriptions);
        }

        // POST: PrescriptionsPatientView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DoctorPrescriptions == null)
            {
                return Problem("Entity set 'TrialRunContext.DoctorPrescriptions'  is null.");
            }
            var doctorPrescriptions = await _context.DoctorPrescriptions.FindAsync(id);
            if (doctorPrescriptions != null)
            {
                _context.DoctorPrescriptions.Remove(doctorPrescriptions);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorPrescriptionsExists(int id)
        {
          return (_context.DoctorPrescriptions?.Any(e => e.prescription_id == id)).GetValueOrDefault();
        }
    }
}
