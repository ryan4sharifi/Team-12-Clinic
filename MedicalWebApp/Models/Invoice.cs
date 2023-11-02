using System;
using System.Collections.Generic;

namespace MedicalWebApp.Models
{
    public partial class Invoice
    {
        public int InvoiceId { get; set; }
        public int PatientId { get; set; }
        public decimal Balance { get; set; }
        public int InsuranceId { get; set; }
        public decimal InsuranceDeduction { get; set; }
        public decimal Copay { get; set; }
        public DateTime DueDate { get; set; }

        public virtual Insurance Insurance { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
    }
}
