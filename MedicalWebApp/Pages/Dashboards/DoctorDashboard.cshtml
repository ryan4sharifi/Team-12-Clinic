using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedicalWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalWebApp.Migrations;

namespace MedicalWebApp.Pages.Dashboards
{
    [Authorize(Roles = "Doctor")]
    public class DoctorDashboardModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly team12MainContext _context;

        [BindProperty]
        public string ReportType { get; set; }
        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }

        public object GeneratedReport { get; set; }
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
            // Handle user not found or not logged in
        }

        public async Task OnPostAsync()
        {
            await OnGetAsync(); // To ensure upcoming appointments are also fetched

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var doctor = _context.Doctors.FirstOrDefault(d => d.IdentityUserId == user.Id);
                if (doctor != null)
                {
                    switch (ReportType)
                    {
                        case "appointmentHistory":
                            GeneratedReport = GenerateAppointmentHistoryReport(StartDate, EndDate, doctor.DoctorId);
                            break;
                        case "patientMedicalHistory":
                            GeneratedReport = GeneratePatientMedicalHistoryReport(StartDate, EndDate, doctor.DoctorId);
                            break;
                            // Handle other report types
                    }
                }
                else
                {
                    // Handle the scenario where the doctor is not found
                }
            }
            else
            {
                // Handle the scenario where the user is not found or not logged in
            }
        }


        private List<AppointmentHistoryViewModel> GenerateAppointmentHistoryReport(DateTime startDate, DateTime endDate, int doctorId)
        {
            return _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.DateAppointment >= startDate && a.DateAppointment <= endDate)
                .Select(a => new AppointmentHistoryViewModel
                {
                    PatientName = $"{a.Patient.FirstName} {a.Patient.LastName}",
                    Date = a.DateAppointment,
                    Time = a.Time
                }).ToList();
        }

        private List<PatientMedicalHistoryViewModel> GeneratePatientMedicalHistoryReport(DateTime startDate, DateTime endDate, int doctorId)
        {
            var reports = _context.MedicalHistories
                          .Where(mh => mh.Patient.Appointments.Any(a => a.DoctorId == doctorId && a.DateAppointment >= startDate && a.DateAppointment <= endDate))
                          .Select(mh => new PatientMedicalHistoryViewModel
                          {
                              PatientName = $"{mh.Patient.FirstName} {mh.Patient.LastName}",
                              DiagnosisInfo = mh.DiagnosisInfo
                              // Map other necessary properties from MedicalHistory
                          }).ToList();
            return reports;
        }

        public class AppointmentViewModel
        {
            public string PatientName { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan Time { get; set; }
            public string FormattedTime => Time.ToString(@"hh\:mm");
        }

        public class AppointmentHistoryViewModel
        {
            public string PatientName { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan Time { get; set; }
        }

        public class PatientMedicalHistoryViewModel
        {
            public string PatientName { get; set; }
            public string DiagnosisInfo { get; set; }
        }
    }
}
