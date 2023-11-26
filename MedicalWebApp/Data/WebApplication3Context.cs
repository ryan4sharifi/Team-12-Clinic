using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;
using med_test8.Models;

namespace WebApplication3.Data
{
    public class WebApplication3Context : DbContext
    {
        public WebApplication3Context (DbContextOptions<WebApplication3Context> options)
            : base(options)
        {
        }

        public DbSet<WebApplication3.Models.Nurses> Nurses { get; set; } = default!;
        public DbSet<WebApplication3.Models.Doctors> Doctors { get; set; }

        public DbSet<WebApplication3.Models.NurseAppointments>? Appointments { get; set; }
        public DbSet<WebApplication3.Models.Tests> Tests { get; set; }
        public DbSet<WebApplication3.Models.Vaccines> Vaccines { get; set; }
        public DbSet<WebApplication3.Models.AppointmentHealthInformation> AppointmentHealthInformation { get; set; }
        public DbSet<WebApplication3.Models.Our_Providers> Our_Providers { get; set; }
        public DbSet<WebApplication3.Models.Patients> Patients { get; set; }
        public DbSet<WebApplication3.Models.VaccinationView> VaccinationView { get; set; }
        public DbSet<WebApplication3.Models.combined_health_view> combined_health_view { get; set; }
        public DbSet<WebApplication3.Models.past_appointments> past_appointments { get; set; }
        public DbSet<WebApplication3.Models.TestDetails> TestDetails { get; set; }
        public DbSet<med_test8.Models.Patients>? Patients_1 { get; set; }
        public DbSet<WebApplication3.Models.MostRecentHealthInfo>? MostRecentHealthInfo { get; set; }
        public DbSet<WebApplication3.Models.PatientPrescriptionView>? PatientPrescriptionView { get; set; }
        public DbSet<WebApplication3.Models.Prescriptions>? Prescriptions { get; set; }


        

    }
}
