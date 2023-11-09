using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MedicalWebApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
    public Appointment Appointment { get; set; }

    [BindProperty]
    public NewPatientRegistrationModel NewPatient { get; set; }

    public SelectList DoctorList { get; set; }

    public class NewPatientRegistrationModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        public DateTime DoB { get; set; }
    }

    public async Task<IActionResult> OnGetAsync()
    {
        DoctorList = new SelectList(await _context.Doctors.Select(d =>
            new { d.DoctorId, DoctorName = d.FirstName + " " + d.LastName })
            .ToListAsync(), "DoctorId", "DoctorName");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            DoctorList = new SelectList(await _context.Doctors.Select(d =>
                new { d.DoctorId, DoctorName = d.FirstName + " " + d.LastName })
                .ToListAsync(), "DoctorId", "DoctorName");
            return Page();
        }

        if (!User.Identity.IsAuthenticated)
        {
            // Register the new patient
            var user = new IdentityUser { UserName = NewPatient.Email, Email = NewPatient.Email };
            var result = await _userManager.CreateAsync(user, NewPatient.Password);

            if (result.Succeeded)
            {
                // Add to 'Patient' role
                await _userManager.AddToRoleAsync(user, "Patient");

                // Create new patient record
                var patient = new Patient
                {
                    FirstName = NewPatient.FirstName,
                    MiddleInitial = NewPatient.MiddleInitial,
                    LastName = NewPatient.LastName,
                    Address = NewPatient.Address,
                    Email = NewPatient.Email,
                    Phone = NewPatient.Phone,
                    Gender = NewPatient.Gender,
                    DoB = NewPatient.DoB,
                    IdentityUserId = user.Id
                };
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();

                // Create appointment
                Appointment.PatientId = patient.PatientId;
                _context.Appointments.Add(Appointment);
                await _context.SaveChangesAsync();

                // Sign in the patient
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToPage("AppointmentConfirmation", new { id = Appointment.AppointmentId });
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        else
        {
            var userId = _userManager.GetUserId(User);
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.IdentityUserId == userId);
            if (patient != null)
            {
                Appointment.PatientId = patient.PatientId;
                _context.Appointments.Add(Appointment);
                await _context.SaveChangesAsync();
                return RedirectToPage("AppointmentConfirmation", new { id = Appointment.AppointmentId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Patient not found.");
            }
        }

        return Page();
    }
}
