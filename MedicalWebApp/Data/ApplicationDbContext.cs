using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using med_test8.Models;

namespace med_test8.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<med_test8.Models.Appointment_SV> Appointment_SV { get; set; } = default!;
        public DbSet<med_test8.Models.Appointments> Appointments { get; set; } = default!;
        public DbSet<med_test8.Models.Patients>? Patients { get; set; }
        public DbSet<med_test8.Models.Doctor_List>? Doctor_List { get; set; }
        public DbSet<med_test8.Models.Doctor_Details>? Doctor_Details { get; set; }
        public DbSet<med_test8.Models.Our_Providers>? Our_Providers { get; set; }
    }
}