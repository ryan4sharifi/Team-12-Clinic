using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrialRun.Data;
using TrialRun.Models;

namespace MedicalWebApp.Controllers
{
    public class RevenueReportController : Controller
    {
        private readonly TrialRunContext _context;

        public RevenueReportController(TrialRunContext context)
        {
            _context = context;
        }

        // GET: RevenueReport
        public async Task<IActionResult> Index(string selectedClassification, string selectedOffice, int? selectedDoctor, string selectedInsurance)
        {
            // Fetch all unique classifications, office locations, and doctors
            var classifications = await _context.RevenueReport
                .Select(r => r.Classification)
                .Distinct()
                .ToListAsync();

            var offices = await _context.RevenueReport
                .Select(r => r.DoctorOffice)
                .Distinct()
                .ToListAsync();

            var doctors = await _context.RevenueReport
                .Select(r => new { Id = r.DoctorId, Name = $"{r.DoctorLastName}" })
                .Distinct()
                .ToListAsync();

            ViewBag.Classifications = classifications;
            ViewBag.Offices = offices;
            ViewBag.Doctors = new SelectList(doctors, "Id", "Name", selectedDoctor);

            // Options for patient insurance filter
            var insuranceOptions = new List<SelectListItem>
    {
        new SelectListItem { Value = "All", Text = "All Patients" },
        new SelectListItem { Value = "WithInsurance", Text = "Patients with Insurance" },
        new SelectListItem { Value = "WithoutInsurance", Text = "Patients without Insurance" }
    };

            ViewBag.InsuranceOptions = new SelectList(insuranceOptions, "Value", "Text", selectedInsurance);

            var appointments = _context.RevenueReport.AsQueryable();

            // If a classification is selected, filter appointments by it
            if (!string.IsNullOrEmpty(selectedClassification))
            {
                appointments = appointments.Where(a => a.Classification == selectedClassification);
            }

            // If an office is selected, filter appointments by it
            if (!string.IsNullOrEmpty(selectedOffice))
            {
                appointments = appointments.Where(a => a.DoctorOffice == selectedOffice);
            }

            // If a doctor is selected, filter appointments by the doctor
            if (selectedDoctor.HasValue)
            {
                appointments = appointments.Where(a => a.DoctorId == selectedDoctor.Value);
            }

            // Filter appointments based on insurance status
            switch (selectedInsurance)
            {
                case "WithInsurance":
                    appointments = appointments.Where(a => a.InsuranceCopay != 1.0);
                    break;
                case "WithoutInsurance":
                    appointments = appointments.Where(a => a.InsuranceCopay == 1.0);
                    break;
                    // No filter for "All Patients"
            }

            // Calculate the sum of PatientCharge for the filtered appointments
            double totalPatientCharge = await appointments.SumAsync(a => a.PatientCharge);

            // Pass the totalPatientCharge to the view
            ViewBag.TotalPatientCharge = totalPatientCharge;

            return View(await appointments.ToListAsync());
        }


        private bool PatientAppointmentIIExists(int id)
        {
          return (_context.PatientAppointmentII?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }
    }
}
