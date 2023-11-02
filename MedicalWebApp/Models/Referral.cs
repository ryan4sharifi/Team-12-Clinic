using System;
using System.Collections.Generic;

namespace MedicalWebApp.Models
{
    public partial class Referral
    {
        public int ReferralId { get; set; }
        public int PrimaryDoctorId { get; set; }
        public int SpecialistDoctorId { get; set; }
        public int SpecialtyId { get; set; }
        public DateTime ReferralDate { get; set; }
        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; } = null!;
        public virtual Doctor PrimaryDoctor { get; set; } = null!;
        public virtual Doctor SpecialistDoctor { get; set; } = null!;
        public virtual Speciality Specialty { get; set; } = null!;
    }
}
