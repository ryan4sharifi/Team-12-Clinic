using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using med_test8.Models;
using System.Security.Claims;
using med_test8.Data;

namespace MedicalWebApp.Controllers
{
    public class AppointmentDoctorViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentDoctorViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AppointmentDoctorView
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Get the currently logged-in user's email
                var loggedInUserEmail = User.FindFirstValue(ClaimTypes.Email);

                // Retrieve appointments only for the logged-in patient based on email
                var patientAppointments = await _context.Appointment_SV
                    .Where(pa => pa.DoctorEmail == loggedInUserEmail)
                    .ToListAsync();

                return View(patientAppointments);
            }

            // Handle the case where the user is not authenticated
            return Challenge();
        }

        // GET: AppointmentDoctorView/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointment_SV == null)
            {
                return NotFound();
            }

            var appointment_SV = await _context.Appointment_SV
                .FirstOrDefaultAsync(m => m.appointment_id == id);
            if (appointment_SV == null)
            {
                return NotFound();
            }

            return View(appointment_SV);
        }

        // GET: AppointmentDoctorView/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppointmentDoctorView/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("appointment_id,DoctorName,PatientName,date_appointment")] Appointment_SV appointment_SV)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment_SV);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment_SV);
        }

        // GET: AppointmentDoctorView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointment_SV == null)
            {
                return NotFound();
            }

            var appointment_SV = await _context.Appointment_SV.FindAsync(id);
            if (appointment_SV == null)
            {
                return NotFound();
            }
            return View(appointment_SV);
        }

        // POST: AppointmentDoctorView/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("appointment_id,DoctorName,PatientName,date_appointment")] Appointment_SV appointment_SV)
        {
            if (id != appointment_SV.appointment_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment_SV);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Appointment_SVExists(appointment_SV.appointment_id))
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
            return View(appointment_SV);
        }

        // GET: AppointmentDoctorView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment_SV = await _context.Appointments
                .FirstOrDefaultAsync(m => m.appointment_id == id);
            if (appointment_SV == null)
            {
                return NotFound();
            }

            return View(appointment_SV);
        }

        // POST: AppointmentDoctorView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'team12MainContext.Appointment_SV'  is null.");
            }
            var appointment_SV = await _context.Appointments.FindAsync(id);
            if (appointment_SV != null)
            {
                _context.Appointments.Remove(appointment_SV);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Appointment_SVExists(int id)
        {
          return (_context.Appointment_SV?.Any(e => e.appointment_id == id)).GetValueOrDefault();
        }
    }
}
