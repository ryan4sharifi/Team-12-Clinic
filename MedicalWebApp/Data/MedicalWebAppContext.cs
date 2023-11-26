using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MedicalWebApp.Models;

namespace MedicalWebApp.Data
{
    public class MedicalWebAppContext : DbContext
    {
        public MedicalWebAppContext (DbContextOptions<MedicalWebAppContext> options)
            : base(options)
        {
        }


    }
}
