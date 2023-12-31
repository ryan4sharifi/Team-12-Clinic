﻿using MedicalWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace TrialRun.Models
{
    public class PatientView
    {

        [Key]
        public int prescription_id { get; set; }

        public int doctor_id { get; set; }

        public int patient_id { get; set; }

        public string drug_name { get; set; }

        public string dosage {  get; set; }

        public int refills { get; set; }

        public DateTime date_prescription { get; set; }

    }
    public class Appointments
    {
        [Key]
        public int appointment_id { get; set; }
        public int patient_id { get; set; }

        public DateTime date_appointment { get; set; }
        public int office_id { get; set; }
        public int doctor_id { get; set; }
    }

    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class Invoices
    {
        [Key]
        public int invoice_id { get; set; }
        public int patient_id { get; set; }

        public decimal balance { get; set; }

        public int insurance_id { get; set; }

        public decimal insurance_deduction { get; set; }

        public decimal copay { get; set; }

        public DateTime due_date { get; set; }

    }
    public class PatientAppointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public int patient_id { get; set; }
        public string PatientLastName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorOffice { get; set; }

    }

    public class PatientAppointmentII
    {
        [Key]
        public int AppointmentId { get; set; }
        public int patient_id { get; set; }
        public string PatientLastName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorOffice { get; set; }
        public string PatientEmail { get; set; }
        public DateTime AppointmentDate { get; set; }
        

    }

    public class RevenueReport
    {
        [Key]
        public int AppointmentId { get; set; }
        public int patient_id { get; set; }
        public string PatientLastName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorOffice { get; set; }
        public string PatientEmail { get; set; }
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }
        public int SpecialtyCost { get; set; }
        public string Classification { get; set; }
        public double InsuranceCopay { get; set; }
        public double PatientCharge { get; set; }


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
        public int speciality_id { get; set; }
        public string office { get; set; }
        [DataType(DataType.Date)]
        public DateTime DoB { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
    }

    public class DoctorSpecialties
    {
        [Key]
        public int doctor_id { get; set; }
        public string first_name { get; set; }
        public string middle_initial { get; set; }
        public string last_name { get; set; }
        public int speciality_id { get; set; }
        public string office { get; set; }
        [DataType(DataType.Date)]
        public DateTime DoB { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string classification { get; set; }
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
        public DateTime date_prescription { get; set; }


      
    }

    public class DoctorPrescriptions // Recycling Table. Will be used for Patient View
    {
        [Key]
        public int prescription_id { get; set; }
        public int doctor_id { get; set; }
        public int patient_id { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string drug_name { get; set; } = null!;
        public string dosage { get; set; } = null!;
        public int refills { get; set; }
        [DataType(DataType.Date)]
        public DateTime date_prescription { get; set; }
        public string doctor_email { get; set; }
        public string patient_email { get; set; }


    }

    public partial class Specialities
    {
        [Key]
        public int speciality_id { get; set; }
        public string classification { get; set; }
    }

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

    public class Notifications
    {
        [Key]
        public int notification_id { get; set; }
        public int patient_id { get; set; }
        public string message { get; set; }
        public DateTime created_at { get; set; }
        public bool is_read { get; set; }
    }


    public class PatientNotifications
    {
        [Key]
        public int notification_id { get; set; }
        public int patient_id { get; set; }
        public string message { get; set; }
        public DateTime created_at { get; set; }
        public bool is_read { get; set; }
        public string patient_email { get; set; }
    }

    public class Referrals
    {
        [Key]
        public int referral_id { get; set; }
        public int specialist_doctor_id { get; set; }
        public int speciality_id { get; set; }
        public DateTime referral_date { get; set; }
        public int patient_id { get; set; }
    }
}
