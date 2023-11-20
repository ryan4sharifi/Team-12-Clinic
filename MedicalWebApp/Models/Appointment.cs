using System;
using System.Collections.Generic;

namespace MedicalWebApp.Models
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public DateTime DateAppointment { get; set; }
        public int OfficeId { get; set; }
        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
    }
}
