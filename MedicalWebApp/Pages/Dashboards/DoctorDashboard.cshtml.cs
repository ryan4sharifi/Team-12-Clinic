using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedicalWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MedicalWebApp.Pages.Dashboards
{
    [Authorize(Roles = "Doctor")]
    public class DoctorDashboardModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly team12MainContext _context; 

        public List<AppointmentViewModel> UpcomingAppointments { get; set; }

        public DoctorDashboardModel(UserManager<IdentityUser> userManager, team12MainContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                
                var doctor = _context.Doctors.FirstOrDefault(d => d.IdentityUserId == user.Id);
                if (doctor != null)
                {
                    UpcomingAppointments = _context.Appointments
                        .Where(a => a.DoctorId == doctor.DoctorId && a.DateAppointment >= System.DateTime.Today)
                        .OrderBy(a => a.DateAppointment)
                        .ThenBy(a => a.Time)
                        .Select(a => new AppointmentViewModel
                        {
                            
                            PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                            Date = a.DateAppointment,
                            Time = a.Time
                            
                        }).ToList();
                }
            }
            else
            {
                // Handle the case where the doctor is not found or user is not logged in
                // This might involve redirecting to an error page or login page
            }
        }
    }

    public class AppointmentViewModel
    {
        public string PatientName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public string FormattedTime => Time.ToString(@"hh\:mm"); 
    }

}
