using System;
using System.Collections.Generic;

namespace MedicalWebApp.Models
{
    public partial class Schedule
    {
        public int ScheduleId { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateSchedule { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual Doctor Doctor { get; set; } = null!;
    }
}
