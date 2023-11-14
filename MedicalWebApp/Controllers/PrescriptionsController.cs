using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrialRun.Data;
using TrialRun.Models;

namespace TrialRun.Controllers
{
    public class PrescriptionsController : Controller
    {
        private readonly TrialRunContext _context;

        public PrescriptionsController(TrialRunContext context)
        {
            _context = context;
        }

        // GET: Prescriptions
        public async Task<IActionResult> Index()
        {
              return _context.Prescriptions != null ? 
                          View(await _context.Prescriptions.ToListAsync()) :
                          Problem("Entity set 'TrialRunContext.Prescriptions'  is null.");
        }

        // GET: Prescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prescriptions == null)
            {
                return NotFound();
            }

            var prescriptions = await _context.Prescriptions
                .FirstOrDefaultAsync(m => m.prescription_id == id);
            if (prescriptions == null)
            {
                return NotFound();
            }

            return View(prescriptions);
        }

        // GET: Prescriptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("prescription_id,doctor_id,patient_id,drug_name,dosage,refills,date_prescription")] PatientView prescriptions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prescriptions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prescriptions);
        }

        // GET: Prescriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prescriptions == null)
            {
                return NotFound();
            }

            var prescriptions = await _context.Prescriptions.FindAsync(id);
            if (prescriptions == null)
            {
                return NotFound();
            }
            return View(prescriptions);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("prescription_id,doctor_id,patient_id,drug_name,dosage,refills,date_prescription")] PatientView prescriptions)
        {
            if (id != prescriptions.prescription_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prescriptions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrescriptionsExists(prescriptions.prescription_id))
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
            return View(prescriptions);
        }

        // GET: Prescriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prescriptions == null)
            {
                return NotFound();
            }

            var prescriptions = await _context.Prescriptions
                .FirstOrDefaultAsync(m => m.prescription_id == id);
            if (prescriptions == null)
            {
                return NotFound();
            }

            return View(prescriptions);
        }

        // POST: Prescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prescriptions == null)
            {
                return Problem("Entity set 'TrialRunContext.Prescriptions'  is null.");
            }
            var prescriptions = await _context.Prescriptions.FindAsync(id);
            if (prescriptions != null)
            {
                _context.Prescriptions.Remove(prescriptions);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrescriptionsExists(int id)
        {
          return (_context.Prescriptions?.Any(e => e.prescription_id == id)).GetValueOrDefault();
        }
    }
}
