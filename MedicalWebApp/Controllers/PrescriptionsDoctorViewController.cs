using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalWebApp.Models;
using System.Security.Claims;

namespace MedicalWebApp.Controllers
{
    public class PrescriptionsDoctorViewController : Controller
    {
        private readonly team12MainContext _context;

        public PrescriptionsDoctorViewController(team12MainContext context)
        {
            _context = context;
        }

        // GET: PrescriptionsDoctorView
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Get the currently logged-in user's email
                var loggedInUserEmail = User.FindFirstValue(ClaimTypes.Email);

                // Retrieve appointments only for the logged-in patient based on email
                var prescriptions = await _context.DoctorPrescriptions
                    .Where(pa => pa.doctor_email == loggedInUserEmail)
                    .ToListAsync();

                return View(prescriptions);
            }

            // Handle the case where the user is not authenticated
            return Challenge();
        }

        // GET: PrescriptionsDoctorView/Details/5
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

        // GET: PrescriptionsDoctorView/Create
        public IActionResult Create()
        {
            var loggedInUserEmail = User.FindFirstValue(ClaimTypes.Email);

            // Retrieve the patient whose email matches the currently logged-in user's email
            var loggedInDoctor = _context.Doctors
                .FirstOrDefault(d => d.Email == loggedInUserEmail);

            if (loggedInDoctor != null)
            {
                // Only include the currently logged-in patient in the dropdown
                ViewBag.Doctors = new List<object>
        {
            new { Id = loggedInDoctor.DoctorId, FullName = $"{loggedInDoctor.LastName}, {loggedInDoctor.FirstName}   {loggedInDoctor.DoctorId}" }
        };
            }
            else
            {
                // Handle the case where the currently logged-in user is not a patient
                ViewBag.Patients = new List<object>();
            }



            ViewBag.Patients = _context.Patients
                .OrderBy(d => d.LastName)
                .ThenBy(d => d.FirstName)
                .Select(d => new { Id = d.PatientId, FullName = $"{d.LastName}, {d.FirstName}   {d.PatientId}" })
                .ToList();

            return View(new Prescriptions());
        }

        // POST: PrescriptionsDoctorView/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("prescription_id,doctor_id,patient_id,drug_name,dosage,refills,date_prescription")] Prescriptions doctorPrescriptions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctorPrescriptions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctorPrescriptions);
        }

        // GET: PrescriptionsDoctorView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prescriptions == null)
            {
                return NotFound();
            }

            var doctorPrescriptions = await _context.Prescriptions.FindAsync(id);
            if (doctorPrescriptions == null)
            {
                return NotFound();
            }
            return View(doctorPrescriptions);
        }

        // POST: PrescriptionsDoctorView/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("prescription_id,doctor_id,patient_id,PatientName,drug_name,dosage,refills,date_prescription,doctor_email")] Prescriptions doctorPrescriptions)
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

        // GET: PrescriptionsDoctorView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prescriptions == null)
            {
                return NotFound();
            }

            var doctorPrescriptions = await _context.Prescriptions
                .FirstOrDefaultAsync(m => m.prescription_id == id);
            if (doctorPrescriptions == null)
            {
                return NotFound();
            }

            return View(doctorPrescriptions);
        }

        // POST: PrescriptionsDoctorView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prescriptions == null)
            {
                return Problem("Entity set 'team12MainContext.DoctorPrescriptions'  is null.");
            }
            var doctorPrescriptions = await _context.Prescriptions.FindAsync(id);
            if (doctorPrescriptions != null)
            {
                _context.Prescriptions.Remove(doctorPrescriptions);
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
