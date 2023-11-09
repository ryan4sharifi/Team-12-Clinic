using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using med_test8.Data;
using med_test8.Models;
using Microsoft.AspNetCore.Authorization;

namespace med_test8.Controllers
{
    //[Authorize]
    public class Appointment_SVController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Appointment_SVController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appointment_SV
        public async Task<IActionResult> Index()
        {
            if (_context.Appointment_SV == null)
            {
                return Problem("Entity set 'med_test7Context.Appointment_SV' is null.");
            }

            var appointments = await _context.Appointment_SV.OrderBy(a => a.date_appointment).ToListAsync();
            return View(appointments);
        }

        // GET: Appointment_SV/Details/5
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

        // GET: CreateAppointment_SV/Create
        public IActionResult Create()
        {
            return View(new Appointments());
        }

        // POST: Appointment_SV/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("doctor_id,patient_id,date_appointment,office_id")] Appointments newAppointment)
        {
            if (ModelState.IsValid)
            {
                // Check for existing appointments for the same doctor within 45 minutes of the new appointment
                var existingAppointments = _context.Appointments
                    .Where(a => a.doctor_id == newAppointment.doctor_id &&
                                a.date_appointment >= newAppointment.date_appointment.AddMinutes(-45) &&
                                a.date_appointment <= newAppointment.date_appointment.AddMinutes(45))
                    .ToList();

                if (existingAppointments.Any())
                {
                    ModelState.AddModelError(string.Empty, "Another appointment with the same doctor is scheduled within 45 minutes of this time. Please try again.");
                    return View(newAppointment);
                }

                _context.Add(newAppointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newAppointment);
        }



        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var Appointments = await _context.Appointments.FindAsync(id);
            if (Appointments == null)
            {
                return NotFound();
            }

            return View(Appointments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("appointment_id,patient_id,date_appointment,office_id,doctor_id")] Appointments Appointments)
        {
            if (id != Appointments.appointment_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Appointments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(Appointments.appointment_id))
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
            return View(Appointments);
        }

        // GET: Appointment_SV/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Appointment_SV/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'med_test7Context.Appointments' is null.");
            }

            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        private bool AppointmentExists(int id)
        {
            return (_context.Appointments?.Any(e => e.appointment_id == id)).GetValueOrDefault();
        }


    }


}