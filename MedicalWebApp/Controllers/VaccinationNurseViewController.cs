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
    public class VaccinationNurseViewController : Controller
    {
        private readonly WebApplication3Context _context;

        public VaccinationNurseViewController(WebApplication3Context context)
        {
            _context = context;
        }

        // GET: VaccinationNurseView
        public async Task<IActionResult> Index()
        {
            return _context.VaccinationView != null ?
                        View(await _context.VaccinationView.ToListAsync()) :
                        Problem("Entity set 'WebApplication3Context.VaccinationView'  is null.");
        }

        // GET: VaccinationNurseView/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VaccinationView == null)
            {
                return NotFound();
            }

            var vaccinationView = await _context.VaccinationView
                .FirstOrDefaultAsync(m => m.vaccination_id == id);
            if (vaccinationView == null)
            {
                return NotFound();
            }

            return View(vaccinationView);
        }

        //My Vaccine Controller
        // GET: Vaccines/Create
        public IActionResult CreateVaccine()
        {
            ViewData["ProviderList"] = GetProviderSelectList();
            ViewData["PatientList"] = GetPatientSelectList();
            return View();
        }

        // POST: Vaccines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVaccine([Bind("patient_id,provider_id,vaccine_name,date_administered")] Vaccines vaccine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If the ModelState is not valid, you need to repopulate the provider dropdown
            ViewData["ProviderList"] = GetProviderSelectList();
            ViewData["PatientList"] = GetPatientSelectList();

            return View(vaccine);   
        }

        // Add this method to your controller
        private SelectList GetProviderSelectList()
        {
            var providers = _context.Our_Providers.ToList(); // Replace "Our_Providers" with the actual DbSet property name
            providers.ForEach(provider => provider.FullName = $"{provider.FullName} - {provider.id}");
            return new SelectList(providers, "id", "FullName");
        }

        private SelectList GetPatientSelectList()
        {
            var patients = _context.Patients.ToList(); // Replace "Our_Providers" with the actual DbSet property name
            patients.ForEach(patients => patients.first_name = $"{patients.last_name},{patients.first_name} {patients.middle_initial} - {patients.patient_id}");
            return new SelectList(patients, "patient_id", "first_name");
        }

        // GET: VaccinationNurseView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vaccines == null)
            {
                return NotFound();
            }

            var vaccinationView = await _context.Vaccines.FindAsync(id);
            if (vaccinationView == null)
            {
                return NotFound();
            }
            return View(vaccinationView);
        }

        // POST: VaccinationNurseView/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("vaccination_id,patient_id,provider_id,vaccine_name,date_administered")] Vaccines vaccinationView)
        {
            if (id != vaccinationView.vaccination_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccinationView);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccinationViewExists(vaccinationView.vaccination_id))
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
            return View(vaccinationView);
        }

        // GET: VaccinationNurseView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vaccines == null)
            {
                return NotFound();
            }

            var vaccinationView = await _context.Vaccines
                .FirstOrDefaultAsync(m => m.vaccination_id == id);
            if (vaccinationView == null)
            {
                return NotFound();
            }

            return View(vaccinationView);
        }

        // POST: VaccinationNurseView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vaccines == null)
            {
                return Problem("Entity set 'WebApplication3Context.VaccinationView'  is null.");
            }
            var vaccinationView = await _context.Vaccines.FindAsync(id);
            if (vaccinationView != null)
            {
                _context.Vaccines.Remove(vaccinationView);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationViewExists(int id)
        {
            return (_context.Vaccines?.Any(e => e.vaccination_id == id)).GetValueOrDefault();
        }
    }
}
