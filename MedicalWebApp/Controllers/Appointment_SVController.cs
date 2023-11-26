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
using System.Net.Mail;
using System.Net;
using Microsoft.Data.SqlClient;
using TrialRun.Models;

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
        public async Task<IActionResult> Index(string sortOrder, string doctorName, string patientName)
        {
            ViewData["LastNameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";

            var appointments = from a in _context.Appointment_SV select a;

            appointments = appointments.Where(a =>
                (string.IsNullOrEmpty(doctorName) || a.DoctorName.Contains(doctorName)) &&
                (string.IsNullOrEmpty(patientName) || a.PatientName.Contains(patientName))
            );

            switch (sortOrder)
            {
                case "lastname_desc":
                    appointments = appointments.OrderByDescending(a => a.PatientName);
                    break;
                case "Date":
                    appointments = appointments.OrderBy(a => a.date_appointment);
                    break;
                case "date_desc":
                    appointments = appointments.OrderByDescending(a => a.date_appointment);
                    break;
                default:
                    appointments = appointments.OrderBy(a => a.PatientName);
                    break;
            }

            return View(await appointments.ToListAsync());
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

        // GET: CreateAppointment_SV/Create
        public IActionResult Create()
        {
            PopulateDoctorsDropdown();
            PopulatePatientsDropdown();

            return View(new med_test8.Models.Appointments());
        }

        // POST: Appointment_SV/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("doctor_id,patient_id,date_appointment,office_id")] med_test8.Models.Appointments newAppointment)
        {

            try
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
            catch (DbUpdateException ex)
            {
                // Handle the exception caused by the trigger
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 16)
                {
                    // The trigger raised a custom error, handle it as needed
                    ModelState.AddModelError(string.Empty, sqlException.Message);
                    return View(newAppointment);
                }

                // Handle other exceptions as needed
                TempData["ShowPopup"] = true;
                TempData["PopupMessage"] = "A referral is required to schedule with this doctor. Please contact your primary doctor to request one.";
                TempData["PopupType"] = "error";
                //ModelState.AddModelError(string.Empty, "An error occurred while saving the appointment.");
                PopulatePatientsDropdown();
                PopulateDoctorsDropdown();
                return View(newAppointment);
            }
        }

        

        private async Task SendAppointmentConfirmationEmail(med_test8.Models.Appointments appointment)
        {
            // Replace these with your actual email settings
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Port = 587,
                Credentials = new NetworkCredential("team12database@gmail.com", "vtzk ushh ggzb yeil"),
                EnableSsl = true,
            };

            var patient = _context.Patients.Find(appointment.patient_id);

            var mailMessage = new MailMessage
            {
                From = new MailAddress("team12database@gmail.com"),
                Subject = "Appointment Confirmation",
                Body = $"Hello, {patient.first_name}. Your appointment on {appointment.date_appointment} has been successfully scheduled."
            };

            mailMessage.To.Add(patient.email); // Replace with the actual customer's email address

            await smtpClient.SendMailAsync(mailMessage);
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
        public async Task<IActionResult> Edit(int id, [Bind("appointment_id,patient_id,date_appointment,office_id,doctor_id")] med_test8.Models.Appointments Appointments)
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