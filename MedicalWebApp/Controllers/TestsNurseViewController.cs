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
    public class TestsNurseViewController : Controller
    {
        private readonly WebApplication3Context _context;

        public TestsNurseViewController(WebApplication3Context context)
        {
            _context = context;
        }

        // GET: TestsNurseView
        public async Task<IActionResult> Index()
        {
              return _context.TestDetails != null ? 
                          View(await _context.TestDetails.ToListAsync()) :
                          Problem("Entity set 'WebApplication3Context.TestDetails'  is null.");
        }

        // GET: TestsNurseView/Details/5
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

        // GET: TestsNurseView/Create
   
        public IActionResult CreateTest()
        {
            ViewBag.Doctors = _context.Doctors
                 .OrderBy(d => d.last_name)
                 .ThenBy(d => d.first_name)
                 .Select(d => new { Id = d.doctor_id, FullName = $"{d.last_name}, {d.first_name}   {d.doctor_id}" })
                 .ToList();

            ViewBag.Patients = _context.Patients
                .OrderBy(d => d.last_name)
                .ThenBy(d => d.first_name)
                .Select(d => new { Id = d.patient_id, FullName = $"{d.last_name}, {d.first_name}   {d.patient_id}" })
                .ToList();

            return View(new Tests());
        }

        // POST: Tests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTest([Bind("patient_id,doctor_id,date_test,status,description")] Tests test)
        {
            if (ModelState.IsValid)
            {
                _context.Add(test);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Doctors = _context.Doctors
                 .OrderBy(d => d.last_name)
                 .ThenBy(d => d.first_name)
                 .Select(d => new { Id = d.doctor_id, FullName = $"{d.last_name}, {d.first_name}   {d.doctor_id}" })
                 .ToList();

            ViewBag.Patients = _context.Patients
                .OrderBy(d => d.last_name)
                .ThenBy(d => d.first_name)
                .Select(d => new { Id = d.patient_id, FullName = $"{d.last_name}, {d.first_name}   {d.patient_id}" })
                .ToList();

            return View(test);
        }

        // GET: TestsNurseView/Edit/5
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

            // Populate the ViewBag with dropdown options
            ViewBag.StatusOptions = new List<SelectListItem>
    {
        new SelectListItem { Text = "Waiting", Value = "waiting" },
        new SelectListItem { Text = "Complete", Value = "complete" }
    };

            return View(testDetails);
        }

        // POST: TestsNurseView/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("test_id,patient_id,PatientName,date_test,doctor_id,DoctorEmail,PatientEmail,status,results,description")] Tests testDetails)
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

        // GET: TestsNurseView/Delete/5
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

        // POST: TestsNurseView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TestDetails == null)
            {
                return Problem("Entity set 'WebApplication3Context.TestDetails'  is null.");
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
          return (_context.TestDetails?.Any(e => e.test_id == id)).GetValueOrDefault();
        }
    }
}
