using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrialRun.Models;

namespace TrialRun.Data
{
    public class TrialRunContext : DbContext
    {
        public TrialRunContext (DbContextOptions<TrialRunContext> options)
            : base(options)
        {
        }

        public DbSet<TrialRun.Models.Patients> Patients { get; set; } = default!;
        public DbSet<TrialRun.Models.Doctors>? Doctors { get; set; }

        public DbSet<TrialRun.Models.Appointments>? Appointments { get; set; }

        public DbSet<TrialRun.Models.PatientAppointment>? PatientAppointment { get; set; }

        public DbSet<TrialRun.Models.Invoices>? Invoices { get; set; }

        public DbSet<TrialRun.Models.Prescriptions>? Prescriptions { get; set; }

        public DbSet<TrialRun.Models.PatientAppointmentII>? PatientAppointmentII { get; set; }
        public DbSet<TrialRun.Models.DoctorPrescriptions>? DoctorPrescriptions { get; set; }
        public DbSet<TrialRun.Models.Specialities>? Specialities { get; set; }
        public DbSet<TrialRun.Models.RevenueReport>? RevenueReport { get; set; }

        



    }
}
