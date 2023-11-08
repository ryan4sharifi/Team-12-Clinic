using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalWebApp.Models;
using System.Threading.Tasks;
using System.Linq;

public class CreateAppointmentModel : PageModel
{
    private readonly team12MainContext _context;

    public CreateAppointmentModel(team12MainContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Appointment Appointment { get; set; }

    public SelectList DoctorList { get; set; }

    public string PatientName { get; set; }

    public async Task<IActionResult> OnGetAsync(int? patientId = null)
    {
        // Fetch the list of doctors and create a SelectList for the dropdown.
        var doctorsQuery = _context.Doctors
            .Select(d => new { d.DoctorId, FullName = d.FirstName + " " + (d.MiddleInitial + " ").Trim() + d.LastName });
        DoctorList = new SelectList(await doctorsQuery.ToListAsync(), "DoctorId", "FullName");

        // If a patientId was passed to the page, find the patient and set the name.
        if (patientId.HasValue)
        {
            var patient = await _context.Patients.FindAsync(patientId.Value);
            if (patient != null)
            {
                PatientName = patient.FirstName + " " + patient.LastName;
                Appointment.PatientId = patientId.Value;
            }
            else
            {
                // Add an error and short-circuit the method by returning to the same page.
                ModelState.AddModelError("", "Patient not found.");
                return Page();
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // Re-populate DoctorList if the model state is not valid to preserve the dropdown list.
            var doctorsQuery = _context.Doctors
                .Select(d => new { d.DoctorId, FullName = d.FirstName + " " + (d.MiddleInitial + " ").Trim() + d.LastName });
            DoctorList = new SelectList(await doctorsQuery.ToListAsync(), "DoctorId", "FullName");

            // Return to the page to display validation errors.
            return Page();
        }

        // Add the new appointment to the context and save changes to the database.
        _context.Appointments.Add(Appointment);
        await _context.SaveChangesAsync();

        // Redirect to an appointment confirmation page with the new appointment's ID.
        return RedirectToPage("./AppointmentConfirmation", new { id = Appointment.AppointmentId });
    }
}
