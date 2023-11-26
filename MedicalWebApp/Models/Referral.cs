using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalWebApp.Models
{
    public partial class Referral
    {
        public int ReferralId { get; set; }
        public int PrimaryDoctorId { get; set; }
        public int SpecialistDoctorId { get; set; }
        public int SpecialityId { get; set; }
        public DateTime ReferralDate { get; set; }
        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; } = null!;
        public virtual Doctor PrimaryDoctor { get; set; } = null!;
        public virtual Doctor SpecialistDoctor { get; set; } = null!;
        public virtual Speciality Specialty { get; set; } = null!;
    }


    public class ReferralView
    {
        [Key]
        public int referral_id { get; set; }
        public string primary_doctor_name { get; set; }
        public string specialist_doctor_name { get; set; }
        public string patient_name { get; set; }
        public string speciality_classification { get; set; }
        public DateTime referral_date { get; set; }
    }

}