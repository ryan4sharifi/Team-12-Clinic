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
    public class TestsTESTController : Controller
    {
        private readonly team12MainContext _context;

        public TestsTESTController(team12MainContext context)
        {
            _context = context;
        }

        // GET: TestsTEST
        public async Task<IActionResult> Index()
        {
              return _context.Tests != null ? 
                          View(await _context.Tests.ToListAsync()) :
                          Problem("Entity set 'team12MainContext.Tests'  is null.");
        }

        // GET: TestsTEST/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tests == null)
            {
                return NotFound();
            }

            var tests = await _context.Tests
                .FirstOrDefaultAsync(m => m.test_id == id);
            if (tests == null)
            {
                return NotFound();
            }

            return View(tests);
        }

        // GET: TestsTEST/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TestsTEST/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("test_id,patient_id,doctor_id,date_test,status,description")] Tests tests)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tests);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tests);
        }

        // GET: TestsTEST/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tests == null)
            {
                return NotFound();
            }

            var tests = await _context.Tests.FindAsync(id);
            if (tests == null)
            {
                return NotFound();
            }
            return View(tests);
        }

        // POST: TestsTEST/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("test_id,patient_id,doctor_id,date_test,status,description")] Tests tests)
        {
            if (id != tests.test_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tests);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestsExists(tests.test_id))
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
            return View(tests);
        }

        // GET: TestsTEST/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tests == null)
            {
                return NotFound();
            }

            var tests = await _context.Tests
                .FirstOrDefaultAsync(m => m.test_id == id);
            if (tests == null)
            {
                return NotFound();
            }

            return View(tests);
        }

        // POST: TestsTEST/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tests == null)
            {
                return Problem("Entity set 'team12MainContext.Tests'  is null.");
            }
            var tests = await _context.Tests.FindAsync(id);
            if (tests != null)
            {
                _context.Tests.Remove(tests);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestsExists(int id)
        {
          return (_context.Tests?.Any(e => e.test_id == id)).GetValueOrDefault();
        }
    }
}
