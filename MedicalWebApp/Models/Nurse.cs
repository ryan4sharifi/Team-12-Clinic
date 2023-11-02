using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MedicalWebApp.Models
{
    public partial class Nurse
    {
        public int NurseId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleInitial { get; set; }
        public string LastName { get; set; } = null!;
        public string? Office { get; set; }
        public DateTime DoB { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string? IdentityUserId { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
    }
}
