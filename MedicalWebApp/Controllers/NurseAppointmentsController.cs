using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class NurseAppointmentsController : Controller
    {
        private readonly WebApplication3Context _context;

        public NurseAppointmentsController(WebApplication3Context context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
              return _context.Appointments != null ? 
                          View(await _context.Appointments.ToListAsync()) :
                          Problem("Entity set 'WebApplication3Context.Appointments'  is null.");
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments
                .FirstOrDefaultAsync(m => m.appointment_id == id);
            if (appointments == null)
            {
                return NotFound();
            }

            return View(appointments);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("appointment_id,patient_id,date_appointment,office_id,doctor_id")] NurseAppointments appointments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointments);
        }


        /*
         ******  Create Test Controller                 ******
         ******  THIS IS A NEW CONTROLLER I'VE CREATED  ******
         ******  TO CONNECT THE TEST TABLE WITH HTML    ******
        */

        // GET: Tests/Create
        public IActionResult CreateTest()
        {
            return View();
        }

        // POST: Tests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTest([Bind("patient_id,doctor_id,date_test,description")] Tests test)
        {
            if (ModelState.IsValid)
            {
                _context.Add(test);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(test);
        }

        //My Vaccine Controller
        // GET: Vaccines/Create
        public IActionResult CreateVaccine()
        {
            return View();
        }

        // POST: Vaccines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVaccine([Bind("patient_id,nurse_id,doctor_id,vaccine_name,date_administered")] Vaccines vaccine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Assuming "Index" is the action to which you want to redirect after adding a vaccine.
            }
            return View(vaccine);
        }

        // My AppointmentHealthInformation Controller

        // GET: AppointmentHealthInformation/Create
        public IActionResult CreateCheckup()
        {
            return View();
        }

        // POST: AppointmentHealthInformation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCheckup([Bind("appointment_id,patient_id,weight_lbs,height_inches,heart_rate,systolic_pressure,diastolic_pressure,temperature_fahrenheit,medications_used,smoke_or_vape,consume_alcohol,allergies")] AppointmentHealthInformation appointmentHealthInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointmentHealthInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Assuming "Index" is the action to which you want to redirect after adding health information.
            }
            return View(appointmentHealthInformation);
        }


        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments.FindAsync(id);
            if (appointments == null)
            {
                return NotFound();
            }
            return View(appointments);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("appointment_id,patient_id,date_appointment,time,office_id,doctor_id")] NurseAppointments appointments)
        {
            if (id != appointments.appointment_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentsExists(appointments.appointment_id))
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
            return View(appointments);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments
                .FirstOrDefaultAsync(m => m.appointment_id == id);
            if (appointments == null)
            {
                return NotFound();
            }

            return View(appointments);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'WebApplication3Context.Appointments'  is null.");
            }
            var appointments = await _context.Appointments.FindAsync(id);
            if (appointments != null)
            {
                _context.Appointments.Remove(appointments);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentsExists(int id)
        {
          return (_context.Appointments?.Any(e => e.appointment_id == id)).GetValueOrDefault();
        }
    }
}
