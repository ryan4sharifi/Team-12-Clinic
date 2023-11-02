using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MedicalWebApp.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
            Prescriptions = new HashSet<Prescription>();
            ReferralPrimaryDoctors = new HashSet<Referral>();
            ReferralSpecialistDoctors = new HashSet<Referral>();
            Schedules = new HashSet<Schedule>();
            Tests = new HashSet<Test>();
        }

        public int DoctorId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleInitial { get; set; }
        public string LastName { get; set; } = null!;
        public int SpecialtyId { get; set; }
        public string? Office { get; set; }
        public DateTime DoB { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string IdentityUserId { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }

        public virtual Speciality Specialty { get; set; } = null!;
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
        public virtual ICollection<Referral> ReferralPrimaryDoctors { get; set; }
        public virtual ICollection<Referral> ReferralSpecialistDoctors { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
