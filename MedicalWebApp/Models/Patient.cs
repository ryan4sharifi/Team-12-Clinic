using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalWebApp.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
            EmergencyContacts = new HashSet<EmergencyContact>();
            Insurances = new HashSet<Insurance>();
            Invoices = new HashSet<Invoice>();
            MedicalHistories = new HashSet<MedicalHistory>();
            Prescriptions = new HashSet<Prescription>();
            Referrals = new HashSet<Referral>();
            Tests = new HashSet<Test>();
        }

        public int PatientId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleInitial { get; set; }
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        [StringLength(10)]
        public string Gender { get; set; } = null!;

        public DateTime DoB { get; set; }
        public decimal Balance { get; set; }

        public string? IdentityUserId { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; }
        public virtual ICollection<Insurance> Insurances { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<MedicalHistory> MedicalHistories { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
        public virtual ICollection<Referral> Referrals { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
