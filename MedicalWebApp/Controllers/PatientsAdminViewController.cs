using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using med_test8.Data;
using med_test8.Models;

namespace MedicalWebApp.Controllers
{
    public class PatientsAdminViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsAdminViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PatientsAdminView
        public async Task<IActionResult> Index()
        {
              return _context.Patients != null ? 
                          View(await _context.Patients.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Patients'  is null.");
        }

        // GET: PatientsAdminView/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patients = await _context.Patients
                .FirstOrDefaultAsync(m => m.patient_id == id);
            if (patients == null)
            {
                return NotFound();
            }

            return View(patients);
        }

        // GET: PatientsAdminView/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatientsAdminView/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("patient_id,first_name,middle_initial,last_name,address,email,phone,gender,DoB,balance")] Patients patients)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patients);
        }

        public IActionResult CreateAppt()
        {
            PopulateDoctorsDropdown();
            PopulatePatientsDropdown();

            return View();
        }

        // POST: Appointment_SV/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAppt([Bind("doctor_id,patient_id,date_appointment,office_id")] Appointments newAppointment)
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
                    PopulateDoctorsDropdown();
                    PopulatePatientsDropdown();
                    return View(newAppointment);
                }

                _context.Add(newAppointment);
                await _context.SaveChangesAsync();

                // Send email to the customer
                // await SendAppointmentConfirmationEmail(newAppointment);

                return RedirectToAction("Index");
            }
            PopulateDoctorsDropdown();
            PopulatePatientsDropdown();
            return View(newAppointment);
        }

        private void PopulateDoctorsDropdown()
        {
            ViewBag.Doctors = _context.Doctors
                .OrderBy(d => d.last_name)
                .ThenBy(d => d.first_name)
                .Select(d => new { Id = d.doctor_id, FullName = $"{d.last_name}, {d.first_name}   {d.doctor_id}" })
                .ToList();
        }

        private void PopulatePatientsDropdown()
        {
            ViewBag.Patients = _context.Patients
                .OrderBy(p => p.last_name)
                .ThenBy(p => p.first_name)
                .Select(p => new { Id = p.patient_id, FullName = $"{p.last_name}, {p.first_name}   {p.patient_id}" })
                .ToList();
        }

        // GET: PatientsAdminView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patients = await _context.Patients.FindAsync(id);
            if (patients == null)
            {
                return NotFound();
            }
            return View(patients);
        }

        // POST: PatientsAdminView/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("patient_id,first_name,middle_initial,last_name,address,email,phone,gender,DoB,balance")] Patients patients)
        {
            if (id != patients.patient_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientsExists(patients.patient_id))
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
            return View(patients);
        }

        // GET: PatientsAdminView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patients = await _context.Patients
                .FirstOrDefaultAsync(m => m.patient_id == id);
            if (patients == null)
            {
                return NotFound();
            }

            return View(patients);
        }

        // POST: PatientsAdminView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patients == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Patients'  is null.");
            }
            var patients = await _context.Patients.FindAsync(id);
            if (patients != null)
            {
                _context.Patients.Remove(patients);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientsExists(int id)
        {
          return (_context.Patients?.Any(e => e.patient_id == id)).GetValueOrDefault();
        }
    }
}
