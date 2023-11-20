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
        public DateTime date_test { get; set; }
        public string status { get; set; }
        public string description { get; set; }
    }

    public class TestDetails
    {
        [Key]
        public int test_id { get; set; }
        public int patient_id { get; set; }
        public string PatientName { get; set; }
        public DateTime date_test { get; set; }
        public int doctor_id { get; set; }
        public string DoctorEmail { get; set; }
        public string PatientEmail { get; set; }
        public string status { get; set; } = null!;
        public string description { get; set; }
    }

    public class Vaccines
    {
        [Key]
        public int vaccination_id { get; set; }
        public int patient_id { get; set; }
        public int? provider_id { get; set; }
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
        public bool smoke_or_vape { get; set; }
        public bool consume_alcohol { get; set; }
        public bool allergies { get; set; }
        public int nurse_id {get; set;}

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

    public class Our_Providers
    {
        [Key]
        public int id { get; set; }
        public string FullName { get; set; }
        public string Office { get; set; }
        public string classification { get; set; }
        public char gender { get; set; }
    }

    public class Patients
    {
        [Key]
        public int patient_id { get; set; }
        public string first_name { get; set; }
        public string middle_initial { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public char gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime DoB { get; set; }
        public decimal balance { get; set; }

    }

    public class VaccinationView
    {
        [Key]
        public int vaccination_id { get; set; }
        public string PatientName { get; set; }
        public string ProviderName { get; set; }
        public string vaccine_name { get; set; }
        [DataType(DataType.Date)]
        public DateTime date_administered { get; set; }
    }

    public class combined_health_view
    {
        [Key]
        public int appointment_id { get; set; }
        public decimal? weight_lbs { get; set; }
        public int? height_inches { get; set; }
        public int? heart_rate { get; set; }
        public int? systolic_pressure { get; set; }
        public int? diastolic_pressure { get; set; }
        public decimal? temperature_fahrenheit { get; set; }
        public bool smoke_or_vape { get; set; }
        public bool consume_alcohol { get; set; }
        public bool allergies { get; set; }
        public int nurse_id { get; set; }
        public int patient_id { get; set; }
        public string PatientName { get; set; }
        public string NurseName { get; set; }
    }

    public class past_appointments
    {
        [Key]
        public int appointment_id { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public DateTime date_appointment { get; set; }
        public int doctor_id { get; set; }
        public int patient_id{ get; set; }
        public string DoctorEmail { get; set; }
        public string PatientEmail { get; set; }
    }

}
