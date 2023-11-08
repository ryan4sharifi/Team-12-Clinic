using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MedicalWebApp.Models
{
    public partial class team12MainContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public team12MainContext()
        {
        }

        public team12MainContext(DbContextOptions<team12MainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<Doctor> Doctors { get; set; } = null!;
        public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; } = null!;
        public virtual DbSet<Insurance> Insurances { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<MedicalHistory> MedicalHistories { get; set; } = null!;
        public virtual DbSet<MetaData> MetaDatas { get; set; } = null!;
        public virtual DbSet<Nurse> Nurses { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<Prescription> Prescriptions { get; set; } = null!;
        public virtual DbSet<Referral> Referrals { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<Speciality> Specialities { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:team12main.database.windows.net,1433;Initial Catalog=team12Main;Persist Security Info=False;User ID=ryan;Password=Amir1995;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            modelBuilder.Entity<IdentityUserClaim<string>>().HasKey(c => c.Id);
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasKey(rc => rc.Id);
            modelBuilder.Entity<IdentityRole<string>>().HasKey(r => r.Id);

            modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole { Id = "1", Name = "Doctor", NormalizedName = "DOCTOR" },
               new IdentityRole { Id = "2", Name = "Patient", NormalizedName = "PATIENT" },
               new IdentityRole { Id = "3", Name = "Admin", NormalizedName = "ADMIN" },
               new IdentityRole { Id = "4", Name = "Nurse", NormalizedName = "NURSE" }
               );
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.DoB).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleInitial)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("middle_initial");

                entity.Property(e => e.Office)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("office");

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("phone");
                entity.Property(e => e.IdentityUserId).IsRequired(false);

                entity.HasOne(a => a.IdentityUser)
                    .WithMany()
                    .HasForeignKey(a => a.IdentityUserId);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.DateAppointment)
                    .HasColumnType("date")
                    .HasColumnName("date_appointment");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.OfficeId).HasColumnName("office_id");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Appointme__docto__778AC167");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Appointme__patie__76969D2E");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.DoB).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleInitial)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("middle_initial");

                entity.Property(e => e.Office)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("office");

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.SpecialityId).HasColumnName("speciality_id");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.SpecialityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Doctors__special__5EBF139D");

                entity.Property(e => e.IdentityUserId)
                    .IsRequired(false);

                entity.HasOne(d => d.IdentityUser)
                    .WithMany()
                    .HasForeignKey(d => d.IdentityUserId);
            });

            modelBuilder.Entity<EmergencyContact>(entity =>
            {
                entity.Property(e => e.EmergencyContactId).HasColumnName("emergency_contact_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleInitial)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("middle_initial");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Relation)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("relation");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.EmergencyContacts)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Emergency__patie__6E01572D");
            });

            modelBuilder.Entity<Insurance>(entity =>
            {
                entity.Property(e => e.InsuranceId).HasColumnName("insurance_id");

                entity.Property(e => e.InsuranceName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("insurance_name");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Insurances)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Insurance__patie__68487DD7");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("balance");

                entity.Property(e => e.Copay)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("copay");

                entity.Property(e => e.DueDate)
                    .HasColumnType("date")
                    .HasColumnName("due_date");

                entity.Property(e => e.InsuranceDeduction)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("insurance_deduction");

                entity.Property(e => e.InsuranceId).HasColumnName("insurance_id");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.HasOne(d => d.Insurance)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.InsuranceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Invoices__insura__06CD04F7");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Invoices__patien__05D8E0BE");
            });

            modelBuilder.Entity<MedicalHistory>(entity =>
            {
                entity.Property(e => e.MedicalHistoryId).HasColumnName("medical_history_id");

                entity.Property(e => e.Allergies)
                    .HasColumnType("text")
                    .HasColumnName("allergies");

                entity.Property(e => e.DiagnosisInfo)
                    .HasColumnType("text")
                    .HasColumnName("diagnosis_info");

                entity.Property(e => e.Medication)
                    .HasColumnType("text")
                    .HasColumnName("medication");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.Surgeries)
                    .HasColumnType("text")
                    .HasColumnName("surgeries");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.MedicalHistories)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MedicalHi__patie__6B24EA82");
            });

            modelBuilder.Entity<MetaData>(entity =>
            {
                entity.Property(e => e.MetaDataId).HasColumnName("meta_data_id");

                entity.Property(e => e.Changelog)
                    .HasColumnType("text")
                    .HasColumnName("changelog");

                entity.Property(e => e.IdentifierNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("identifier_number");

                entity.Property(e => e.LoginInfo)
                    .HasColumnType("text")
                    .HasColumnName("login_info");
            });

            modelBuilder.Entity<Nurse>(entity =>
            {
                entity.Property(e => e.NurseId).HasColumnName("nurse_id");

                entity.Property(e => e.DoB).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleInitial)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("middle_initial");

                entity.Property(e => e.Office)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("office");

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.IdentityUserId).IsRequired(false);

                entity.HasOne(n => n.IdentityUser)
                    .WithMany()
                    .HasForeignKey(n => n.IdentityUserId);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("balance");

                entity.Property(e => e.DoB).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("gender")
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleInitial)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("middle_initial");

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.IdentityUserId).IsRequired(false);

                entity.HasOne(p => p.IdentityUser)
                    .WithMany()
                    .HasForeignKey(p => p.IdentityUserId);
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.Property(e => e.PrescriptionId).HasColumnName("prescription_id");

                entity.Property(e => e.DatePrescription)
                    .HasColumnType("date")
                    .HasColumnName("date_prescription");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.Dosage)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("dosage");

                entity.Property(e => e.DrugName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("drug_name");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.Refills).HasColumnName("refills");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Prescript__docto__7F2BE32F");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Prescript__patie__00200768");
            });

            modelBuilder.Entity<Referral>(entity =>
            {
                entity.Property(e => e.ReferralId).HasColumnName("referral_id");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.PrimaryDoctorId).HasColumnName("primary_doctor_id");

                entity.Property(e => e.ReferralDate)
                    .HasColumnType("date")
                    .HasColumnName("referral_date");

                entity.Property(e => e.SpecialistDoctorId).HasColumnName("specialist_doctor_id");

                entity.Property(e => e.SpecialityId).HasColumnName("speciality_id");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Referrals)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Referrals__patie__73BA3083");

                entity.HasOne(d => d.PrimaryDoctor)
                    .WithMany(p => p.ReferralPrimaryDoctors)
                    .HasForeignKey(d => d.PrimaryDoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Referrals__prima__70DDC3D8");

                entity.HasOne(d => d.SpecialistDoctor)
                    .WithMany(p => p.ReferralSpecialistDoctors)
                    .HasForeignKey(d => d.SpecialistDoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Referrals__speci__71D1E811");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.Referrals)
                    .HasForeignKey(d => d.SpecialityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Referrals__speci__72C60C4A");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.DateSchedule)
                    .HasColumnType("date")
                    .HasColumnName("date_schedule");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Schedules__docto__02FC7413");
            });

            modelBuilder.Entity<Speciality>(entity =>
            {
                entity.Property(e => e.SpecialityId).HasColumnName("speciality_id");

                entity.Property(e => e.Classification)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("classification");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.Property(e => e.DateTest)
                    .HasColumnType("date")
                    .HasColumnName("date_test");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.Results)
                    .HasColumnType("text")
                    .HasColumnName("results");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tests__doctor_id__7C4F7684");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tests__patient_i__7B5B524B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
