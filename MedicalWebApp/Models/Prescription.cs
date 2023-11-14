namespace MedicalWebApp.Models
{
    public class Prescription
    {
       
        
            public int PrescriptionId { get; set; }
            public int DoctorId { get; set; }
            public int PatientId { get; set; }
            public string DrugName { get; set; } = null!;
            public string Dosage { get; set; } = null!;
            public int Refills { get; set; }
            public DateTime DatePrescription { get; set; }

            public virtual Doctor Doctor { get; set; } = null!;
            public virtual Patient Patient { get; set; } = null!;
        }
    }

