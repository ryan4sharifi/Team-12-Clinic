using System;
using System.Collections.Generic;

namespace MedicalWebApp.Models
{
    public partial class Speciality
    {
        public Speciality()
        {
            Doctors = new HashSet<Doctor>();
            Referrals = new HashSet<Referral>();
        }

        public int SpecialityId { get; set; }
        public string Classification { get; set; } = null!;

        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Referral> Referrals { get; set; }
    }
}
