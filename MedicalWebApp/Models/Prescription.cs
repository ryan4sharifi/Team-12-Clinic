using System.ComponentModel.DataAnnotations;

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

    public class DoctorPrescriptions
    {
        [Key]
        public int prescription_id { get; set; }
        public int doctor_id { get; set; }
        public int patient_id { get; set; }
        public string PatientName { get; set; }
        public string drug_name { get; set; } = null!;
        public string dosage { get; set; } = null!;
        public int refills { get; set; }
        [DataType(DataType.Date)]
        public DateTime date_prescription { get; set; }
        public string doctor_email { get; set; }
        

    }

    public class Prescriptions
    {
        [Key]
        public int prescription_id { get; set; }
        public int doctor_id { get; set; }
        public int patient_id { get; set; }
        public string drug_name { get; set; } = null!;
        public string dosage { get; set; } = null!;
        public int refills { get; set; }
        [DataType(DataType.Date)]
        public DateTime date_prescription { get; set; }

    }

}


