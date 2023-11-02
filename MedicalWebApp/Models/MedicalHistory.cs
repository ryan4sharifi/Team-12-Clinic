using System;
using System.Collections.Generic;

namespace MedicalWebApp.Models
{
    public partial class MedicalHistory
    {
        public int MedicalHistoryId { get; set; }
        public string? DiagnosisInfo { get; set; }
        public string? Surgeries { get; set; }
        public string? Medication { get; set; }
        public string? Allergies { get; set; }
        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; } = null!;
    }
}
