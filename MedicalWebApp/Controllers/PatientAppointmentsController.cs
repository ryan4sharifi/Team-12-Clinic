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

namespace TrialRun.Controllers
{
    public class PatientAppointmentsController : Controller
    {
        private readonly TrialRunContext _context;

        public PatientAppointmentsController(TrialRunContext context)
        {
            _context = context;
        }

        // GET: PatientAppointments
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Get the currently logged-in user's email
                var loggedInUserEmail = User.FindFirstValue(ClaimTypes.Email);

                // Retrieve appointments only for the logged-in patient based on email
                var patientAppointments = await _context.PatientAppointmentII
                    .Where(pa => pa.PatientEmail == loggedInUserEmail)
                    .ToListAsync();

                return View(patientAppointments);
            }

            // Handle the case where the user is not authenticated
            return Challenge();
        }

        // GET: PatientAppointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PatientAppointment == null)
            {
                return NotFound();
            }

            var patientAppointment = await _context.PatientAppointment
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (patientAppointment == null)
            {
                return NotFound();
            }

            return View(patientAppointment);
        }

        // GET: PatientAppointments/Create
        public IActionResult Create()
        {
            var loggedInUserEmail = User.FindFirstValue(ClaimTypes.Email);

            // Retrieve the patient whose email matches the currently logged-in user's email
            var loggedInPatient = _context.Patients
                .FirstOrDefault(p => p.email == loggedInUserEmail);

            if (loggedInPatient != null)
            {
                // Only include the currently logged-in patient in the dropdown
                ViewBag.Patients = new List<object>
        {
            new { Id = loggedInPatient.patient_id, FullName = $"{loggedInPatient.last_name}, {loggedInPatient.first_name}   {loggedInPatient.patient_id}" }
        };
            }
            else
            {
                // Handle the case where the currently logged-in user is not a patient
                ViewBag.Patients = new List<object>();
            }

            ViewBag.Doctors = _context.Doctors
                .OrderBy(d => d.last_name)
                .ThenBy(d => d.first_name)
                .Select(d => new { Id = d.doctor_id, FullName = $"{d.last_name}, {d.first_name}   {d.doctor_id}" })
                .ToList();

            return View(new Appointments());
        }

        // POST: PatientAppointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,patient_id,doctor_id,date_appointment,office_id")] Appointments patientAppointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientAppointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patientAppointment);
        }

        // GET: PatientAppointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PatientAppointment == null)
            {
                return NotFound();
            }

            var patientAppointment = await _context.PatientAppointment.FindAsync(id);
            if (patientAppointment == null)
            {
                return NotFound();
            }
            return View(patientAppointment);
        }

        // POST: PatientAppointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,patient_id,PatientLastName,DoctorId,DoctorLastName,DoctorOffice")] PatientAppointment patientAppointment)
        {
            if (id != patientAppointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientAppointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientAppointmentExists(patientAppointment.AppointmentId))
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
            return View(patientAppointment);
        }

        // GET: PatientAppointments/Delete/5
        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var patientAppointment = await _context.Appointments
                .FirstOrDefaultAsync(m => m.appointment_id == id);
            if (patientAppointment == null)
            {
                return NotFound();
            }

            return View(patientAppointment);
        }

        // POST: PatientAppointments/Delete/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PatientAppointment == null)
            {
                return Problem("Entity set 'TrialRunContext.PatientAppointment'  is null.");
            }
            var patientAppointment = await _context.Appointments.FindAsync(id);
            if (patientAppointment != null)
            {
                _context.Appointments.Remove(patientAppointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientAppointmentExists(int id)
        {
          return (_context.PatientAppointment?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }
    }
}
