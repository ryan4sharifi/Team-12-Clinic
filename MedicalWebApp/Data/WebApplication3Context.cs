using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public class WebApplication3Context : DbContext
    {
        public WebApplication3Context (DbContextOptions<WebApplication3Context> options)
            : base(options)
        {
        }

        public DbSet<WebApplication3.Models.Nurses> Nurses { get; set; } = default!;

        public DbSet<WebApplication3.Models.NurseAppointments>? Appointments { get; set; }
        public DbSet<WebApplication3.Models.Tests> Tests { get; set; }
        public DbSet<WebApplication3.Models.Vaccines> Vaccines { get; set; }
        public DbSet<WebApplication3.Models.AppointmentHealthInformation> AppointmentHealthInformation { get; set; }
    }
}
