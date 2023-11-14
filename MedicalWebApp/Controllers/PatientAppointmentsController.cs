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
              return _context.PatientAppointment != null ? 
                          View(await _context.PatientAppointment.ToListAsync()) :
                          Problem("Entity set 'TrialRunContext.PatientAppointment'  is null.");
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
            return View();
        }

        // POST: PatientAppointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,patient_id,PatientLastName,DoctorId,DoctorLastName,DoctorOffice")] PatientAppointment patientAppointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientAppointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patientAppointment);
        }

        // GET: PatientAppointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PatientAppointment == null)
            {
                return NotFound();
            }

            var patientAppointment = await _context.PatientAppointment.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,patient_id,PatientLastName,DoctorId,DoctorLastName,DoctorOffice")] PatientAppointment patientAppointment)
        {
            if (id != patientAppointment.AppointmentId)
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
                    if (!PatientAppointmentExists(patientAppointment.AppointmentId))
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
        public async Task<IActionResult> Delete(int? id)
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

        // POST: PatientAppointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PatientAppointment == null)
            {
                return Problem("Entity set 'TrialRunContext.PatientAppointment'  is null.");
            }
            var patientAppointment = await _context.PatientAppointment.FindAsync(id);
            if (patientAppointment != null)
            {
                _context.PatientAppointment.Remove(patientAppointment);
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
