using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace med_test8.Models
{
    public class Appointment_SV
    {
        [Key]
        public int appointment_id { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public DateTime date_appointment { get; set; }
        public string DoctorEmail { get; set; }
    }

    public class Appointments
    {
        [Key]
        public int appointment_id { get; set; }
        public int patient_id { get; set; }
        public DateTime date_appointment { get; set; }
        public int office_id { get; set; }
        public int doctor_id { get; set; }
        // Navigation properties to establish the relationships
        //public Doctor Doctor { get; set; }
        //public Patient Patient { get; set; }
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


    public class Doctors
    {
        [Key]
        public int doctor_id { get; set; }
        public string first_name { get; set; }
        public string middle_initial { get; set; }
        public string last_name { get; set; }
        public int specialty_id { get; set; }
        public string office { get; set; }
        [DataType(DataType.Date)]
        public DateTime DoB { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
    }

    public class Doctor_Details
    {
        [Key]
        public int doctor_id { get; set; }
        public string first_name { get; set; }
        public string middle_initial { get; set; }
        public string last_name { get; set; }
        public string office { get; set; }
        [DataType(DataType.Date)]
        public DateTime DoB { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string classification { get; set; }
    }

    public class Speciality
    {
        [Key]
        public int speciality_id { get; set; }
        public string classification { get; set; }

        //public ICollection<Doctor> Doctors { get; set; }
    }

    public class Doctor_List
    {
        [Key]
        public int doctor_id { get; set; }
        public string DoctorName { get; set; }
        public string Office { get; set; }
        public string classification { get; set; }

    }

    public class Our_Providers
    {
        [Key]
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Office { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Classification { get; set; }
        public char gender { get; set; }
    }

    public class Provider_Info
    {
        [Key]
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Office { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Classification { get; set; }
    }


}


