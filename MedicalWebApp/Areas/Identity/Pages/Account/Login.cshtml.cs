using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Area("Identity")]
public class LoginModel : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    [BindProperty]
    public InputModel Input { get; set; }

    public string ErrorMessage { get; set; }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public LoginModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public void OnGet()
    {
        // Any initialization code can go here
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                // Redirect the user to the appropriate dashboard based on their role.
                var user = await _userManager.FindByEmailAsync(Input.Email);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                {
                    return RedirectToPage("/Dashboards/AdminDashboard");
                }
                else if (roles.Contains("Doctor"))
                {
                    return RedirectToPage("/Dashboards/DoctorDashboard");
                }
                else if (roles.Contains("Nurse"))
                {
                    return RedirectToPage("/Dashboards/NurseDashboard");
                }
                else if (roles.Contains("Patient"))
                {
                    return RedirectToPage("/Dashboards/PatientDashboard");
                }
                else
                {
                    // If the user role is not recognized, redirect to the homepage or a generic post-login page.
                    return RedirectToPage("/Index");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
        }

        // If we got this far, something failed, redisplay form
        return Page();
    }
}
