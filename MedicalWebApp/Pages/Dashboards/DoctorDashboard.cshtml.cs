using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedicalWebApp.Pages.Dashboards
{

    [Authorize(Roles = "Doctor")]
    public class DoctorDashboardModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
