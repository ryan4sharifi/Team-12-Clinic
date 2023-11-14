using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalWebApp.Models;

namespace MedicalWebApp.Controllers
{
    public class MedicalHistoryController : Controller
    {
        private readonly team12MainContext _context;

        public MedicalHistoryController(team12MainContext context)
        {
            _context = context;
        }

        // GET: MedicalHistory
        public async Task<IActionResult> Index()
        {
            var team12MainContext = _context.MedicalHistories.Include(m => m.Patient);
            return View(await team12MainContext.ToListAsync());
        }

        // GET: MedicalHistory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MedicalHistories == null)
            {
                return NotFound();
            }

            var medicalHistory = await _context.MedicalHistories
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.MedicalHistoryId == id);
            if (medicalHistory == null)
            {
                return NotFound();
            }

            return View(medicalHistory);
        }

        // GET: MedicalHistory/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId");
            return View();
        }

        // POST: MedicalHistory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicalHistoryId,DiagnosisInfo,Surgeries,Medication,Allergies,PatientId")] MedicalHistory medicalHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicalHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", medicalHistory.PatientId);
            return View(medicalHistory);
        }

        // GET: MedicalHistory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MedicalHistories == null)
            {
                return NotFound();
            }

            var medicalHistory = await _context.MedicalHistories.FindAsync(id);
            if (medicalHistory == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", medicalHistory.PatientId);
            return View(medicalHistory);
        }

        // POST: MedicalHistory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicalHistoryId,DiagnosisInfo,Surgeries,Medication,Allergies,PatientId")] MedicalHistory medicalHistory)
        {
            if (id != medicalHistory.MedicalHistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalHistoryExists(medicalHistory.MedicalHistoryId))
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
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", medicalHistory.PatientId);
            return View(medicalHistory);
        }

        // GET: MedicalHistory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MedicalHistories == null)
            {
                return NotFound();
            }

            var medicalHistory = await _context.MedicalHistories
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.MedicalHistoryId == id);
            if (medicalHistory == null)
            {
                return NotFound();
            }

            return View(medicalHistory);
        }

        // POST: MedicalHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MedicalHistories == null)
            {
                return Problem("Entity set 'team12MainContext.MedicalHistories'  is null.");
            }
            var medicalHistory = await _context.MedicalHistories.FindAsync(id);
            if (medicalHistory != null)
            {
                _context.MedicalHistories.Remove(medicalHistory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalHistoryExists(int id)
        {
          return (_context.MedicalHistories?.Any(e => e.MedicalHistoryId == id)).GetValueOrDefault();
        }
    }
}
