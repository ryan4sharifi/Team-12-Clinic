﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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
            PopulatePatientsDropdown();
            PopulateDoctorsDropdown();

            return View(new Appointments());
        }

        // POST: PatientAppointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,patient_id,doctor_id,date_appointment,office_id")] Appointments patientAppointment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Your existing code to add the appointment
                    _context.Add(patientAppointment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                // Handle the exception caused by the trigger
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 16)
                {
                    // The trigger raised a custom error, handle it as needed
                    ModelState.AddModelError(string.Empty, sqlException.Message);
                    return View(patientAppointment);
                }

                // Handle other exceptions as needed
                TempData["ShowPopup"] = true;
                TempData["PopupMessage"] = "A referral is required to schedule with this doctor. Please contact your primary doctor to request one.";
                TempData["PopupType"] = "error";
                //ModelState.AddModelError(string.Empty, "An error occurred while saving the appointment.");
                PopulatePatientsDropdown();
                PopulateDoctorsDropdown();
                return View(patientAppointment);
            }

            // ModelState is not valid, so repopulate dropdowns and return to the view
            PopulatePatientsDropdown();
            PopulateDoctorsDropdown();
            return View(patientAppointment);
        }

        private void PopulatePatientsDropdown()
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
        }

        private void PopulateDoctorsDropdown()
        {
            ViewBag.Doctors = _context.DoctorSpecialties
                .OrderBy(d => d.last_name)
                .ThenBy(d => d.first_name)
                .Select(d => new { Id = d.doctor_id, FullName = $"{d.last_name}, {d.first_name} ID{d.doctor_id}  [ {d.classification} ] " })
                .ToList();
        }

        // GET: PatientAppointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var patientAppointment = await _context.Appointments.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("appointment_id,patient_id,doctor_id,office_id,date_appointment")] Appointments patientAppointment)
        {
            if (id != patientAppointment.appointment_id)
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
                    if (!PatientAppointmentExists(patientAppointment.appointment_id))
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
