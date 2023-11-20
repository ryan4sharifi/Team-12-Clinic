using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace MedicalWebApp.Controllers
{
    public class HealthInfoNurseViewController : Controller
    {
        private readonly WebApplication3Context _context;

        public HealthInfoNurseViewController(WebApplication3Context context)
        {
            _context = context;
        }

        // GET: HealthInfoNurseView
        public async Task<IActionResult> Index()
        {
              return _context.combined_health_view != null ? 
                          View(await _context.combined_health_view.ToListAsync()) :
                          Problem("Entity set 'WebApplication3Context.combined_health_view'  is null.");
        }

        // GET: HealthInfoNurseView/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.combined_health_view == null)
            {
                return NotFound();
            }

            var combined_health_view = await _context.combined_health_view
                .FirstOrDefaultAsync(m => m.appointment_id == id);
            if (combined_health_view == null)
            {
                return NotFound();
            }

            return View(combined_health_view);
        }

        // GET: AppointmentHealthInformation/Create
        public IActionResult CreateCheckup()
        {
            var loggedInUserEmail = User.FindFirstValue(ClaimTypes.Email);

            // Retrieve the patient whose email matches the currently logged-in user's email
            var loggedInNurse = _context.Nurses
                .FirstOrDefault(d => d.email == loggedInUserEmail);

            if (loggedInNurse != null)
            {
                // Only include the currently logged-in patient in the dropdown
                ViewBag.Nurses = new List<object>
        {
            new { Id = loggedInNurse.nurse_id, FullName = $"{loggedInNurse.last_name}, {loggedInNurse.first_name}   {loggedInNurse.nurse_id}" }
        };
            }

            ViewData["AppointmentList"] = GetPastAppointmentsList();
            return View();
        }

        // POST: AppointmentHealthInformation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCheckup([Bind("appointment_id,weight_lbs,height_inches,heart_rate,systolic_pressure,diastolic_pressure,temperature_fahrenheit,smoke_or_vape,consume_alcohol,allergies, nurse_id")] AppointmentHealthInformation appointmentHealthInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointmentHealthInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Assuming "Index" is the action to which you want to redirect after adding health information.
            }
            ViewData["AppointmentList"] = GetPastAppointmentsList();
            return View(appointmentHealthInformation);
        }

        private SelectList GetPastAppointmentsList()
        {
            var appointments = _context.past_appointments.ToList(); // Replace "Our_Providers" with the actual DbSet property name
            appointments.ForEach(appointments => appointments.PatientName = $" {appointments.appointment_id} - Patient: {appointments.PatientName}, appt date: {appointments.date_appointment}");
            return new SelectList(appointments, "appointment_id", "PatientName");
        }


        // GET: HealthInfoNurseView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AppointmentHealthInformation == null)
            {
                return NotFound();
            }

            var combined_health_view = await _context.AppointmentHealthInformation.FindAsync(id);
            if (combined_health_view == null)
            {
                return NotFound();
            }
            return View(combined_health_view);
        }

        // POST: HealthInfoNurseView/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("appointment_id,weight_lbs,height_inches,heart_rate,systolic_pressure,diastolic_pressure,temperature_fahrenheit,smoke_or_vape,consume_alcohol,allergies,nurse_id,patient_id,PatientName,NurseName")] AppointmentHealthInformation combined_health_view)
        {
            if (id != combined_health_view.appointment_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(combined_health_view);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!combined_health_viewExists(combined_health_view.appointment_id))
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
            return View(combined_health_view);
        }

        // GET: HealthInfoNurseView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AppointmentHealthInformation == null)
            {
                return NotFound();
            }

            var combined_health_view = await _context.AppointmentHealthInformation
                .FirstOrDefaultAsync(m => m.appointment_id == id);
            if (combined_health_view == null)
            {
                return NotFound();
            }

            return View(combined_health_view);
        }

        // POST: HealthInfoNurseView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AppointmentHealthInformation == null)
            {
                return Problem("Entity set 'WebApplication3Context.combined_health_view'  is null.");
            }
            var combined_health_view = await _context.AppointmentHealthInformation.FindAsync(id);
            if (combined_health_view != null)
            {
                _context.AppointmentHealthInformation.Remove(combined_health_view);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool combined_health_viewExists(int id)
        {
          return (_context.AppointmentHealthInformation?.Any(e => e.appointment_id == id)).GetValueOrDefault();
        }
    }
}
