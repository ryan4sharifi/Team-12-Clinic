using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedicalWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MedicalWebApp.Models.Appointment_SV> Appointment_SV { get; set; } = default!;
        public DbSet<MedicalWebApp.Models.Appointments> Appointments { get; set; } = default!;
        public DbSet<MedicalWebApp.Models.Patients>? Patients { get; set; }
        public DbSet<MedicalWebApp.Models.Doctor_List>? Doctor_List { get; set; }
        public DbSet<MedicalWebApp.Models.Doctor_Details>? Doctor_Details { get; set; }
    }
}