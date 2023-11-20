using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedicalWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

[Area("Identity")]
public class RegisterModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly team12MainContext _context;
    [BindProperty]
    public int DoctorSpecialtyId { get; set; }

    public List<SelectListItem> SpecialityItems { get; set; }


    public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, team12MainContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    [BindProperty]
    public string Role { get; set; }

    // Method to populate roles
    public void PopulateRoles()
    {
        ViewData["Roles"] = new List<string> { "Admin", "Doctor", "Nurse", "Patient" };
    }

    public void OnGet()
    {
        PopulateRoles();

        SpecialityItems = _context.Specialities
            .Select(s => new SelectListItem
            {
                Value = s.SpecialityId.ToString(),
                Text = s.Classification
            }).ToList();


    }


    public async Task<IActionResult> OnPostAsync()
    {
        PopulateRoles();
        

        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                switch (Role)
                {

                    case "Doctor":
                        var doctorFirstName = Request.Form["DoctorFirstName"];
                        var doctorLastName = Request.Form["DoctorLastName"];
                        var doctorMiddleInitial = Request.Form["DoctorMiddleInitial"];
                        var doctorOffice = Request.Form["DoctorOffice"];
                        var doctorDoB = DateTime.TryParse(Request.Form["DoctorDoB"], out DateTime doctorParsedDate) ? doctorParsedDate : DateTime.MinValue;
                        var doctorPhone = Request.Form["DoctorPhone"];
                        var doctorSpecialtyId = int.Parse(Request.Form["DoctorSpecialtyId"]); // Assuming a select dropdown for specialties
                        var doctorGender = Request.Form["DoctorGender"];

                        var doctor = new Doctor
                        {
                            IdentityUserId = user.Id,
                            FirstName = doctorFirstName,
                            MiddleInitial = doctorMiddleInitial,
                            LastName = doctorLastName,
                            Email = Input.Email,
                            Office = doctorOffice,
                            DoB = doctorDoB,
                            Phone = doctorPhone,
                            SpecialityId = doctorSpecialtyId,
                            Gender = doctorGender
                        };

                        _context.Doctors.Add(doctor);

                        await _userManager.UpdateAsync(user);

                        break;

                    case "Admin":
                        var adminFirstName = Request.Form["AdminFirstName"];
                        var adminLastName = Request.Form["AdminLastName"];
                        var adminMiddleInitial = Request.Form["AdminMiddleInitial"];
                        var adminOffice = Request.Form["AdminOffice"];
                        var adminDoB = DateTime.TryParse(Request.Form["AdminDoB"], out DateTime adminParsedDate) ? adminParsedDate : DateTime.MinValue;
                        var adminPhone = Request.Form["AdminPhone"];
                        _context.Admins.Add(new Admin
                        {
                            IdentityUserId = user.Id,
                            FirstName = adminFirstName,
                            MiddleInitial = adminMiddleInitial,
                            LastName = adminLastName,
                            Email = Input.Email,
                            Office = adminOffice,
                            DoB = adminDoB,
                            Phone = adminPhone
                        });
                        break;


                    case "Nurse":
                        var nurseFirstName = Request.Form["NurseFirstName"];
                        var nurseLastName = Request.Form["NurseLastName"];
                        var nurseMiddleInitial = Request.Form["NurseMiddleInitial"];
                        var nurseOffice = Request.Form["NurseOffice"];
                        var nurseDoB = DateTime.TryParse(Request.Form["NurseDoB"], out DateTime nurseParsedDate) ? nurseParsedDate : DateTime.MinValue;
                        var nursePhone = Request.Form["NursePhone"];
                        var nurseGender = Request.Form["NurseGender"];
                        var newNurse = new Nurse
                        
                        {
                            IdentityUserId = user.Id,
                            FirstName = nurseFirstName,
                            MiddleInitial = nurseMiddleInitial,
                            LastName = nurseLastName,
                            Email = Input.Email,
                            Office = nurseOffice,
                            DoB = nurseDoB,
                            Phone = nursePhone,
                            Gender = nurseGender
                        };

                        _context.Nurses.Add(newNurse);

                        await _userManager.UpdateAsync(user);

                        break;

                    case "Patient":
                        var patientFirstName = Request.Form["PatientFirstName"];
                        var patientMiddleInitial = Request.Form["PatientMiddleInitial"];
                        var patientLastName = Request.Form["PatientLastName"];
                        var patientAddress = Request.Form["PatientAddress"];
                        var patientPhone = Request.Form["PatientPhone"];
                        var patientGender = Request.Form["PatientGender"];
                        var patientDoB = DateTime.TryParse(Request.Form["PatientDoB"], out DateTime parsedDate) ? parsedDate : DateTime.MinValue;

                        var newPatient = new Patient
                        {
                            IdentityUserId = user.Id,
                            FirstName = patientFirstName,
                            MiddleInitial = patientMiddleInitial,
                            LastName = patientLastName,
                            Email = Input.Email,
                            Address = patientAddress,
                            Phone = patientPhone,
                            Gender = patientGender,
                            DoB = patientDoB,

                        };

                        // Add the new patient to the Patients table
                        _context.Patients.Add(newPatient);

                        // Save changes to the database
                        await _context.SaveChangesAsync();

                        break;
                }

                await _userManager.AddToRoleAsync(user, Role);
                await _signInManager.SignInAsync(user, isPersistent: false);
                // After user is signed in, redirect based on role
                switch (Role)
                {
                    case "Admin":
                        return RedirectToPage("/Dashboards/AdminDashboard");
                    case "Doctor":
                        return RedirectToPage("/Dashboards/DoctorDashboard");
                    case "Nurse":
                        return RedirectToPage("/Dashboards/NurseDashboard");
                    case "Patient":
                        return RedirectToPage("/Dashboards/PatientDashboard");
                    default:
                        return RedirectToPage("Index");
                }

            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return Page();
    }
}
