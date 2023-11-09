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
    public class Doctor_ListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Doctor_ListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Doctor_List
        public async Task<IActionResult> Index()
        {
              return _context.Doctor_List != null ? 
                          View(await _context.Doctor_List.ToListAsync()) :
                          Problem("Entity set 'med_test7Context.Doctor_List'  is null.");
        }

        // GET: Doctor_List/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Doctor_Details == null)
            {
                return NotFound();
            }

            var doctor_details = await _context.Doctor_Details
                .FirstOrDefaultAsync(m => m.doctor_id == id);
            if (doctor_details == null)
            {
                return NotFound();
            }

            return View(doctor_details);
        }

        // GET: Doctor_List/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctor_List/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("doctor_id,DoctorName,Office,classification")] Doctor_List doctor_List)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctor_List);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor_List);
        }

        // GET: Doctor_List/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Doctor_List == null)
            {
                return NotFound();
            }

            var doctor_List = await _context.Doctor_List.FindAsync(id);
            if (doctor_List == null)
            {
                return NotFound();
            }
            return View(doctor_List);
        }

        // POST: Doctor_List/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("doctor_id,DoctorName,Office,classification")] Doctor_List doctor_List)
        {
            if (id != doctor_List.doctor_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor_List);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Doctor_ListExists(doctor_List.doctor_id))
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
            return View(doctor_List);
        }

        // GET: Doctor_List/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Doctor_List == null)
            {
                return NotFound();
            }

            var doctor_List = await _context.Doctor_List
                .FirstOrDefaultAsync(m => m.doctor_id == id);
            if (doctor_List == null)
            {
                return NotFound();
            }

            return View(doctor_List);
        }

        // POST: Doctor_List/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doctor_List == null)
            {
                return Problem("Entity set 'med_test7Context.Doctor_List'  is null.");
            }
            var doctor_List = await _context.Doctor_List.FindAsync(id);
            if (doctor_List != null)
            {
                _context.Doctor_List.Remove(doctor_List);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Doctor_ListExists(int id)
        {
          return (_context.Doctor_List?.Any(e => e.doctor_id == id)).GetValueOrDefault();
        }
    }
}
