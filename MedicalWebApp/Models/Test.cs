using System;
using System.Collections.Generic;

namespace MedicalWebApp.Models
{
    public partial class Test
    {
        public int TestId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateTest { get; set; }
        public TimeSpan Time { get; set; }
        public string Status { get; set; } = null!;
        public string? Results { get; set; }

        public virtual Doctor Doctor { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
    }
}
