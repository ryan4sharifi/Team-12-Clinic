using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace WebApplication3.Models
{
    public class NurseAppointments
    {
        [Key]
        public int appointment_id { get; set; }
        public int patient_id { get; set; }
        [DataType(DataType.Date)]
        public DateTime date_appointment { get; set; }
        public int office_id { get; set; }
        public int doctor_id { get; set; }
    }


    /* Im creating a new model so we can interact with the Tests table in 
    on the nurse view appointments page */
    public class Tests
    {
        [Key]
        public int test_id { get; set; }
        public int patient_id { get; set; }
        public int doctor_id { get; set; }
        [DataType(DataType.Date)]
        public DateTime date_test { get; set; }
        public TimeSpan time { get; set; }
        public string description { get; set; }
    }

    public class Vaccines
    {
        [Key]
        public int vaccination_id { get; set; }
        public int patient_id { get; set; }
        public int? doctor_id { get; set; }
        public int? nurse_id { get; set; }
        public string vaccine_name { get; set; }
        [DataType(DataType.Date)]
        public DateTime date_administered { get; set; }
    }

    public class AppointmentHealthInformation
    {
        [Key]
        public int appointment_id { get; set; }
        public decimal? weight_lbs { get; set; }
        public int? height_inches { get; set; }
        public int? heart_rate { get; set; }
        public int? systolic_pressure { get; set; }
        public int? diastolic_pressure { get; set; }
        public decimal? temperature_fahrenheit { get; set; }
        public string medications_used { get; set; }
        public string? smoke_or_vape { get; set; }
        public string? consume_alcohol { get; set; }
        public string allergies { get; set; }
    }
    

    public class Doctors
    {
        [Key]
        public int doctor_id { get; set; }
        public string first_name { get; set; } = null!;
        public string? middle_initial { get; set; }
        public string last_name { get; set; } = null!;
        public int specialty_id { get; set; }
        public string? office { get; set; }
        public DateTime DoB { get; set; }
        public string phone { get; set; } = null!;
        public string email { get; set; } = null!;

    }
    public class Nurses
    {
        [Key]
        public int nurse_id { get; set; }
        public string first_name { get; set; } = null!;
        public string? middle_initial { get; set; }
        public string last_name { get; set; } = null!;
        public string? office { get; set; }
        public DateTime DoB { get; set; }
        public string phone { get; set; } = null!;
        public string email { get; set; } = null!;

    }
}
