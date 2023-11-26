using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrialRun.Data;
using TrialRun.Models;

namespace MedicalWebApp.Controllers
{
    public class NotificationsPatientViewController : Controller
    {
        private readonly TrialRunContext _context;

        public NotificationsPatientViewController(TrialRunContext context)
        {
            _context = context;
        }

        // GET: Notifications
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Get the currently logged-in user's email
                var loggedInUserEmail = User.FindFirstValue(ClaimTypes.Email);

                // Retrieve appointments only for the logged-in patient based on email
                var notifications = await _context.PatientNotifications
                    .Where(pa => pa.patient_email == loggedInUserEmail)
                    .ToListAsync();

                return View(notifications);
            }

            // Handle the case where the user is not authenticated
            return Challenge();
        }

        // GET: NotificationsPatientView/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Notifications == null)
            {
                return NotFound();
            }

            var notifications = await _context.Notifications
                .FirstOrDefaultAsync(m => m.notification_id == id);
            if (notifications == null)
            {
                return NotFound();
            }

            return View(notifications);
        }

        // GET: NotificationsPatientView/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NotificationsPatientView/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("notification_id,patient_id,message,created_at,is_read")] Notifications notifications)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notifications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notifications);
        }

        // GET: NotificationsPatientView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Notifications == null)
            {
                return NotFound();
            }

            var notifications = await _context.Notifications.FindAsync(id);
            if (notifications == null)
            {
                return NotFound();
            }
            return View(notifications);
        }

        // POST: NotificationsPatientView/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("notification_id,patient_id,message,created_at,is_read")] Notifications notifications)
        {
            if (id != notifications.notification_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notifications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificationsExists(notifications.notification_id))
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
            return View(notifications);
        }

        // GET: NotificationsPatientView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Notifications == null)
            {
                return NotFound();
            }

            var notifications = await _context.Notifications
                .FirstOrDefaultAsync(m => m.notification_id == id);
            if (notifications == null)
            {
                return NotFound();
            }

            return View(notifications);
        }

        // POST: NotificationsPatientView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Notifications == null)
            {
                return Problem("Entity set 'TrialRunContext.Notifications'  is null.");
            }
            var notifications = await _context.Notifications.FindAsync(id);
            if (notifications != null)
            {
                _context.Notifications.Remove(notifications);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificationsExists(int id)
        {
          return (_context.Notifications?.Any(e => e.notification_id == id)).GetValueOrDefault();
        }

        [HttpGet]
        public IActionResult CheckUnreadNotifications()
        {
            string userEmail = User.Identity.Name;

            // Retrieve the patient_id based on the user's email
            int? patientId = _context.Patients
                .Where(p => p.email == userEmail)
                .Select(p => (int?)p.patient_id)
                .FirstOrDefault();

            if (patientId.HasValue)
            {
                // Check if there are unread notifications for the patient
                bool hasUnreadNotifications = _context.Notifications
                    .Any(n => n.patient_id == patientId && !n.is_read);

                return Json(new { hasUnreadNotifications });
            }
            else
            {
                // Redirect to login page when patient is not found
                return RedirectToPage("/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult MarkAllAsRead()
        {
            string userEmail = User.Identity.Name;

            // Retrieve the patient_id based on the user's email
            int? patientId = _context.Patients
                .Where(p => p.email == userEmail)
                .Select(p => (int?)p.patient_id)
                .FirstOrDefault();

            if (patientId.HasValue)
            {
                // Mark all unread notifications for the patient as read
                var unreadNotifications = _context.Notifications
                    .Where(n => n.patient_id == patientId && !n.is_read)
                    .ToList();

                foreach (var notification in unreadNotifications)
                {
                    notification.is_read = true;
                }

                _context.SaveChanges();

                return Json(new { success = true });
            }
            else
            {
                // Handle the case where the user's email doesn't match any patient
                return RedirectToPage("/Identity/Account/Login");
            }
        }
    }


}
