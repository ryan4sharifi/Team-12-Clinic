using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MedicalWebApp.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;

public class ScheduleAppointmentModel : PageModel
{
    private readonly team12MainContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public ScheduleAppointmentModel(team12MainContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [BindProperty]
    public NewPatientAppointmentModel NewPatientAppointment { get; set; }

    public class NewPatientAppointmentModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime DoB { get; set; }

        [Required]
        public DateTime DateAppointment { get; set; }

        [Required]
        public TimeSpan Time { get; set; }
        // Add other properties as needed for the appointment
    }

    public async Task<IActionResult> OnGetAsync()
    {
        // Load necessary data for the form, e.g., doctor list
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // Reload necessary data
            return Page();
        }

        if (!User.Identity.IsAuthenticated)
        {
            // Check if email exists in the database
            var existingUser = await _userManager.FindByEmailAsync(NewPatientAppointment.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "An account with this email already exists. Please log in.");
                return Page();
            }

            // Create new patient and appointment logic here
            var patient = new Patient
            {
                FirstName = NewPatientAppointment.FirstName,
                LastName = NewPatientAppointment.LastName,
                Email = NewPatientAppointment.Email,
                Phone = NewPatientAppointment.Phone,
                Address = NewPatientAppointment.Address,
                Gender = NewPatientAppointment.Gender,
                DoB = NewPatientAppointment.DoB,
                // Additional patient details initialization
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            var appointment = new Appointment
            {
                PatientId = patient.PatientId,
                DateAppointment = NewPatientAppointment.DateAppointment,
                Time = NewPatientAppointment.Time,
                // Additional appointment details initialization
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Redirect to a confirmation page or display a success message
            return RedirectToPage("AppointmentConfirmation", new { id = appointment.AppointmentId });
        }

        // Logic for handling authenticated users (existing patients)
        // ...

        return Page();
    }
}

