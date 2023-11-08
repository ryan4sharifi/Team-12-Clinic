using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalWebApp.Models;
using med_test8.Models;

namespace MedicalWebApp.Controllers
{
    public class Appointment_SVController : Controller
    {
        private readonly team12MainContext _context;

        public Appointment_SVController(team12MainContext context)
        {
            _context = context;
        }

        // GET: Appointment_SV
        public async Task<IActionResult> Index()
        {
              return _context.Appointment_SV != null ? 
                          View(await _context.Appointment_SV.ToListAsync()) :
                          Problem("Entity set 'team12MainContext.Appointment_SV'  is null.");
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

        // GET: Appointment_SV/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appointment_SV/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("appointment_id,DoctorName,PatientName,date_appointment")] Appointment_SV appointment_SV)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment_SV);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment_SV);
        }

        // GET: Appointment_SV/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointment_SV == null)
            {
                return NotFound();
            }

            var appointment_SV = await _context.Appointment_SV.FindAsync(id);
            if (appointment_SV == null)
            {
                return NotFound();
            }
            return View(appointment_SV);
        }

        // POST: Appointment_SV/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("appointment_id,DoctorName,PatientName,date_appointment")] Appointment_SV appointment_SV)
        {
            if (id != appointment_SV.appointment_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment_SV);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Appointment_SVExists(appointment_SV.appointment_id))
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
            return View(appointment_SV);
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
            if (_context.Appointment_SV == null)
            {
                return Problem("Entity set 'team12MainContext.Appointment_SV'  is null.");
            }
            var appointment_SV = await _context.Appointment_SV.FindAsync(id);
            if (appointment_SV != null)
            {
                _context.Appointment_SV.Remove(appointment_SV);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Appointment_SVExists(int id)
        {
          return (_context.Appointment_SV?.Any(e => e.appointment_id == id)).GetValueOrDefault();
        }
    }
}
