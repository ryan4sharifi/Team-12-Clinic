using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class NewReferrals
    {
        [Key]
        public int referral_id { get; set; }

        [Required]
        public int primary_doctor_id { get; set; }

        [Required]
        public int specialist_doctor_id { get; set; }

        [Required]
        public int speciality_id { get; set; }

        [Required]
        public DateTime referral_date { get; set; }

        [Required]
        public int patient_id { get; set; }
    }
}
