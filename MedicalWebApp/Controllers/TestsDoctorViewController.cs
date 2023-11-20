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
    public class TestsDoctorViewController : Controller
    {
        private readonly team12MainContext _context;

        public TestsDoctorViewController(team12MainContext context)
        {
            _context = context;
        }

        // GET: TestsDoctorView
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Get the currently logged-in user's email
                var loggedInUserEmail = User.FindFirstValue(ClaimTypes.Email);

                // Retrieve appointments only for the logged-in patient based on email
                var submittedTests = await _context.TestDetails
                    .Where(pa => pa.DoctorEmail == loggedInUserEmail)
                    .ToListAsync();

                return View(submittedTests);
            }

            // Handle the case where the user is not authenticated
            return Challenge();
        }

        // GET: TestsDoctorView/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TestDetails == null)
            {
                return NotFound();
            }

            var testDetails = await _context.TestDetails
                .FirstOrDefaultAsync(m => m.test_id == id);
            if (testDetails == null)
            {
                return NotFound();
            }

            return View(testDetails);
        }

        // GET: TestsDoctorView/Create
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

            return View(new Tests());
        }


        // POST: TestsDoctorView/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("test_id,patient_id,doctor_id,date_test,status,description")] Tests testDetails)
        {

            if (ModelState.IsValid)
            {
                _context.Add(testDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

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

          

            return View(testDetails);
        }

        // GET: TestsDoctorView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tests == null)
            {
                return NotFound();
            }

            var testDetails = await _context.Tests.FindAsync(id);
            if (testDetails == null)
            {
                return NotFound();
            }
            return View(testDetails);
        }

        // POST: TestsDoctorView/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("test_id,patient_id,date_test,doctor_id,status,description")] Tests testDetails)
        {
            if (id != testDetails.test_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestDetailsExists(testDetails.test_id))
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
            return View(testDetails);
        }

        // GET: TestsDoctorView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tests == null)
            {
                return NotFound();
            }

            var testDetails = await _context.Tests
                .FirstOrDefaultAsync(m => m.test_id == id);
            if (testDetails == null)
            {
                return NotFound();
            }

            return View(testDetails);
        }

        // POST: TestsDoctorView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tests == null)
            {
                return Problem("Entity set 'team12MainContext.TestDetails'  is null.");
            }
            var testDetails = await _context.Tests.FindAsync(id);
            if (testDetails != null)
            {
                _context.Tests.Remove(testDetails);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestDetailsExists(int id)
        {
          return (_context.Tests?.Any(e => e.test_id == id)).GetValueOrDefault();
        }
    }
}
