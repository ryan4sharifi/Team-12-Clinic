using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalWebApp.Models
{
    public partial class Test
    {
        public int TestId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateTest { get; set; }
        public string Status { get; set; }

        public virtual Doctor Doctor { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
    }

    public partial class Tests
    {
        [Key]
        public int test_id { get; set; }
        public int patient_id { get; set; }
        public int doctor_id { get; set; }
        public DateTime date_test { get; set; }
        public string status { get; set; }
        public string description { get; set; }
    }

    public class TestDetails
    {
        [Key]
        public int test_id { get; set; }
        public int patient_id { get; set; }
        public string PatientName { get; set; }
        public DateTime date_test { get; set; }
        public int doctor_id { get; set; }
        public string DoctorEmail { get; set; }
        public string PatientEmail { get; set; }
        public string status { get; set; } = null!;
        public string description { get; set; }
    }
}
