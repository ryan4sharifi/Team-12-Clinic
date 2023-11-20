using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace MedicalWebApp.Controllers
{
    public class PrescriptionReportController : Controller
    {
        private readonly WebApplication3Context _context;

        public PrescriptionReportController(WebApplication3Context context)
        {
            _context = context;
        }

        // GET: PrescriptionReport
        public async Task<IActionResult> Index(string genderFilter, int? minAge, int? maxAge, string prescriptionName)
        {
            IQueryable<PatientPrescriptionView> query = _context.PatientPrescriptionView;

            if (!string.IsNullOrEmpty(genderFilter))
            {
                query = query.Where(p => p.gender == genderFilter);
            }

            if (minAge.HasValue)
            {
                query = query.Where(p => p.age >= minAge.Value);
            }

            if (maxAge.HasValue)
            {
                query = query.Where(p => p.age <= maxAge.Value);
            }

            if (!string.IsNullOrEmpty(prescriptionName))
            {
                query = query.Where(p => p.drug_name == prescriptionName);
            }

            ViewBag.PrescriptionNames = await _context.Prescriptions.Select(p => p.drug_name).Distinct().ToListAsync();

            List<PatientPrescriptionView> model = await query.ToListAsync();

            return View(model);
        }

        private bool PatientPrescriptionViewExists(int id)
        {
          return (_context.PatientPrescriptionView?.Any(e => e.patient_id == id)).GetValueOrDefault();
        }
    }
}
