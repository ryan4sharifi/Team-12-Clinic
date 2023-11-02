using System;
using System.Collections.Generic;

namespace MedicalWebApp.Models
{
    public partial class EmergencyContact
    {
        public int EmergencyContactId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleInitial { get; set; }
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int PatientId { get; set; }
        public string Relation { get; set; } = null!;

        public virtual Patient Patient { get; set; } = null!;
    }
}
