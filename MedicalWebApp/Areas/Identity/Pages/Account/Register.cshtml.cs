using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedicalWebApp.Models;

[Area("Identity")]
public class RegisterModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly team12MainContext _context;

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

        [Required]
        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime DoB { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                var newPatient = new Patient
                {
                    IdentityUserId = user.Id,
                    FirstName = Input.FirstName,
                    MiddleInitial = Input.MiddleInitial,
                    LastName = Input.LastName,
                    Email = Input.Email,
                    Address = Input.Address,
                    Phone = Input.Phone,
                    Gender = Input.Gender,
                    DoB = Input.DoB
                };

                _context.Patients.Add(newPatient);
                await _context.SaveChangesAsync();

                await _userManager.AddToRoleAsync(user, "Patient");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToPage("/Dashboards/PatientDashboard");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return Page();
    }
}
