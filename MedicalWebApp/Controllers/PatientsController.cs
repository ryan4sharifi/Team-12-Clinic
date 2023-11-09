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

namespace med_test8.Controllers
{
    //[Authorize]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
              return _context.Patients != null ? 
                          View(await _context.Patients.ToListAsync()) :
                          Problem("Entity set 'med_test7Context.Patients'  is null.");
        }

        // GET: Patients/Details/5
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

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
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

        // GET: Patients/Edit/5
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

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("patient_id,last_name,first_name,middle_initial,address,email,phone,gender,DoB,balance")] Patients patients)
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

        // GET: Patients/Delete/5 

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

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patients == null)
            {
                return Problem("Entity set 'med_test7Context.Patients'  is null.");
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
