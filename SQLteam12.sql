/****** Object:  Database [team12Main]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE DATABASE [team12Main]  (EDITION = 'GeneralPurpose', SERVICE_OBJECTIVE = 'GP_S_Gen5_1', MAXSIZE = 6 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS, LEDGER = OFF;
GO
ALTER DATABASE [team12Main] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [team12Main] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [team12Main] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [team12Main] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [team12Main] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [team12Main] SET ARITHABORT OFF 
GO
ALTER DATABASE [team12Main] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [team12Main] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [team12Main] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [team12Main] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [team12Main] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [team12Main] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [team12Main] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [team12Main] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [team12Main] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [team12Main] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [team12Main] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [team12Main] SET  MULTI_USER 
GO
ALTER DATABASE [team12Main] SET ENCRYPTION ON
GO
ALTER DATABASE [team12Main] SET QUERY_STORE = ON
GO
ALTER DATABASE [team12Main] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[patient_id] [int] IDENTITY(90000,1) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[middle_initial] [varchar](50) NULL,
	[last_name] [varchar](50) NOT NULL,
	[address] [varchar](255) NOT NULL,
	[email] [varchar](255) NOT NULL,
	[phone] [varchar](13) NOT NULL,
	[gender] [nvarchar](10) NULL,
	[DoB] [date] NOT NULL,
	[balance] [decimal](10, 2) NOT NULL,
	[IdentityUserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[patient_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[invoice_id] [int] IDENTITY(6300000,1) NOT NULL,
	[patient_id] [int] NOT NULL,
	[balance] [decimal](10, 2) NOT NULL,
	[insurance_id] [int] NOT NULL,
	[insurance_deduction] [decimal](10, 2) NOT NULL,
	[copay] [decimal](10, 2) NULL,
	[due_date] [date] NOT NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[invoice_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[PatientInvoice]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREATE VIEW DoctorsPatientEmergencyContactList AS
--SELECT
 --  d.last_name AS DoctorLastName,
  -- p.last_name AS PatientLastName,
   --p.first_name AS PatientFirstName,
   --p.patient_id,
   --e.emergency_contact_id,
  -- e.phone,
  -- e.last_name AS EmergencyContactLastName,
  -- e.first_name AS EmergencyContactFirstName
--FROM
   --dbo.EmergencyContacts AS e,
  -- dbo.Patients AS p,
 --  dbo.Doctors AS d;

CREATE VIEW [dbo].[PatientInvoice] AS
SELECT 
   p.patient_id,
   p.last_name AS PatientLastName,
   i.balance AS Balance,
   i.copay AS Copay,
   i.due_date AS DueDate
FROM
   dbo.Patients AS p
   JOIN dbo.Invoices AS i ON p.patient_id = i.patient_id






GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[appointment_id] [int] IDENTITY(110000,1) NOT NULL,
	[patient_id] [int] NOT NULL,
	[date_appointment] [datetime] NOT NULL,
	[office_id] [int] NULL,
	[doctor_id] [int] NOT NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[appointment_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctors]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[doctor_id] [int] IDENTITY(1000,1) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[middle_initial] [varchar](50) NULL,
	[last_name] [varchar](50) NOT NULL,
	[speciality_id] [int] NOT NULL,
	[office] [varchar](255) NULL,
	[DoB] [date] NOT NULL,
	[phone] [varchar](13) NOT NULL,
	[email] [varchar](255) NOT NULL,
	[IdentityUserId] [nvarchar](450) NULL,
	[gender] [nchar](2) NULL,
 CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED 
(
	[doctor_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[DoctorsPatientList]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DoctorsPatientList] AS
SELECT DISTINCT
    P.[patient_id] AS PatientId,
    P.[first_name] AS PatientFirstName,
    P.[middle_initial] AS PatientMiddleInitial,
    P.[last_name] AS PatientLastName,
    P.[address] AS PatientAddress,
    P.[email] AS PatientEmail,
    P.[phone] AS PatientPhone,
    P.[gender] AS PatientGender,
    P.[DoB] AS PatientDateOfBirth,
    P.[balance] AS PatientBalance,
    D.[doctor_id] AS DoctorId,
    D.[email] AS DoctorEmail,
    D.[first_name] AS DoctorFirstName
FROM 
    Patients P
JOIN 
    Appointments A ON P.[patient_id] = A.[patient_id]
JOIN 
    Doctors D ON A.[doctor_id] = D.[doctor_id];
GO
/****** Object:  Table [dbo].[Prescriptions]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prescriptions](
	[prescription_id] [int] IDENTITY(12300000,1) NOT NULL,
	[doctor_id] [int] NOT NULL,
	[patient_id] [int] NOT NULL,
	[drug_name] [varchar](255) NOT NULL,
	[dosage] [varchar](100) NOT NULL,
	[refills] [int] NOT NULL,
	[date_prescription] [date] NOT NULL,
 CONSTRAINT [PK_Prescriptions] PRIMARY KEY CLUSTERED 
(
	[prescription_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[DoctorPrescriptions]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DoctorPrescriptions] AS
SELECT
    P.prescription_id,
    P.doctor_id,
    P.patient_id,
    P.drug_name,
    P.dosage,
    P.refills,
    P.date_prescription,
    D.email AS doctor_email,
    Patient.email AS patient_email,
	CONCAT(d.last_name, ', ', d.first_name, ' ', d.middle_initial) AS DoctorName,
    CONCAT(patient.last_name, ', ', patient.first_name, ' ', patient.middle_initial) AS PatientName
FROM
    dbo.Prescriptions P
    INNER JOIN dbo.Doctors D ON P.doctor_id = D.doctor_id
	INNER JOIN Patients patient ON P.patient_id = patient.patient_id;
GO
/****** Object:  View [dbo].[PatientAppointmentII]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREATE VIEW DoctorsPatientEmergencyContactList AS
--SELECT
 --  d.last_name AS DoctorLastName,
  -- p.last_name AS PatientLastName,
   --p.first_name AS PatientFirstName,
   --p.patient_id,
   --e.emergency_contact_id,
  -- e.phone,
  -- e.last_name AS EmergencyContactLastName,
  -- e.first_name AS EmergencyContactFirstName
--FROM
   --dbo.EmergencyContacts AS e,
  -- dbo.Patients AS p,
 --  dbo.Doctors AS d;

--CREATE VIEW PatientInvoice AS
--SELECT 
   --p.patient_id,
  -- p.last_name AS PatientLastName,
  -- i.balance AS Balance,
   --i.copay AS Copay,
  -- i.due_date AS DueDate
--FROM
  -- dbo.Patients AS p
--   JOIN dbo.Invoices AS i ON p.patient_id = i.patient_id

--Select * FROM PatientAppointmentII;

CREATE VIEW [dbo].[PatientAppointmentII] AS
SELECT        
    a.appointment_id AS AppointmentId,        
    p.patient_id,        
    p.last_name + ', ' + p.first_name AS PatientLastName,        
    d.doctor_id AS DoctorId,        
    d.last_name + ', ' + d.first_name AS DoctorLastName,        
    d.office AS DoctorOffice,        
    p.email AS PatientEmail,        
    a.date_appointment AS AppointmentDate
	
FROM
    Appointments a
INNER JOIN Patients p ON a.patient_id = p.patient_id
INNER JOIN Doctors d ON a.doctor_id = d.doctor_id
GO
/****** Object:  Table [dbo].[Insurances]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Insurances](
	[insurance_id] [int] NOT NULL,
	[patient_id] [int] NOT NULL,
	[insurance_name] [varchar](255) NOT NULL,
	[copay] [float] NOT NULL,
 CONSTRAINT [PK_Insurances] PRIMARY KEY CLUSTERED 
(
	[insurance_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialities]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialities](
	[speciality_id] [int] IDENTITY(100,1) NOT NULL,
	[classification] [varchar](255) NOT NULL,
	[cost] [int] NULL,
 CONSTRAINT [PK_Specialities] PRIMARY KEY CLUSTERED 
(
	[speciality_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[RevenueReport]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[RevenueReport] AS
SELECT        
    a.appointment_id AS AppointmentId,        
    p.patient_id,        
    p.last_name + ', ' + p.first_name AS PatientLastName,        
    d.doctor_id AS DoctorId,        
    d.last_name + ', ' + d.first_name AS DoctorLastName,        
    d.office AS DoctorOffice,        
    p.email AS PatientEmail,        
    a.date_appointment AS AppointmentDate,
    s.cost AS SpecialtyCost,   
	s.classification AS Classification,
    COALESCE(i.copay, 1.0) AS InsuranceCopay,    
    (s.cost * COALESCE(i.copay, 1)) AS PatientCharge
	
FROM
    Appointments a
INNER JOIN Patients p ON a.patient_id = p.patient_id
INNER JOIN Doctors d ON a.doctor_id = d.doctor_id
INNER JOIN Specialities s ON d.speciality_id = s.speciality_id
LEFT JOIN Insurances i ON p.patient_id = i.patient_id

-- Add WHERE clause to filter appointments up to the current date
WHERE a.date_appointment <= GETDATE();
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[notification_id] [int] IDENTITY(7770000,1) NOT NULL,
	[patient_id] [int] NULL,
	[message] [text] NULL,
	[created_at] [datetime] NULL,
	[is_read] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[notification_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[PatientNotifications]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PatientNotifications] AS
SELECT
    N.notification_id,
    N.patient_id,
    P.email AS patient_email,
    N.message,
    N.created_at,
    N.is_read
FROM
    dbo.Notifications N
JOIN
    dbo.Patients P ON N.patient_id = P.patient_id;
GO
/****** Object:  View [dbo].[DoctorSpecialties]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DoctorSpecialties] AS
SELECT
    d.doctor_id,
    d.first_name,
    d.middle_initial,
    d.last_name,
    d.speciality_id,
    s.classification, -- Assuming the name column in the Specialities table is 'speciality_name'
    d.office,
    d.DoB,
    d.phone,
    d.email,
    d.IdentityUserId,
    d.gender
FROM
    Doctors d
JOIN
    Specialities s ON d.speciality_id = s.speciality_id;
GO
/****** Object:  Table [dbo].[Referrals]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Referrals](
	[referral_id] [int] IDENTITY(330000000,1) NOT NULL,
	[primary_doctor_id] [int] NOT NULL,
	[specialist_doctor_id] [int] NOT NULL,
	[speciality_id] [int] NOT NULL,
	[referral_date] [date] NOT NULL,
	[patient_id] [int] NOT NULL,
 CONSTRAINT [PK_Referrals] PRIMARY KEY CLUSTERED 
(
	[referral_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ReferralView]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ReferralView]
AS
SELECT
    R.[referral_id],
    Pd.[first_name] + ' ' + Pd.[last_name] AS [primary_doctor_name],
    Sd.[first_name] + ' ' + Sd.[last_name] AS [specialist_doctor_name],
    Pat.[first_name] + ' ' + Pat.[last_name] AS [patient_name],
    Spe.[classification] AS [speciality_classification],
    R.[referral_date]
FROM
    [dbo].[Referrals] R
JOIN
    [dbo].[Doctors] Pd ON R.[primary_doctor_id] = Pd.[doctor_id]
JOIN
    [dbo].[Doctors] Sd ON R.[specialist_doctor_id] = Sd.[doctor_id]
JOIN
    [dbo].[Patients] Pat ON R.[patient_id] = Pat.[patient_id]
JOIN
    [dbo].[Specialities] Spe ON R.[speciality_id] = Spe.[speciality_id];
GO
/****** Object:  Table [dbo].[Nurses]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nurses](
	[nurse_id] [int] IDENTITY(3000,1) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[middle_initial] [varchar](50) NULL,
	[last_name] [varchar](50) NOT NULL,
	[office] [varchar](255) NULL,
	[DoB] [date] NOT NULL,
	[phone] [varchar](13) NOT NULL,
	[email] [varchar](255) NOT NULL,
	[IdentityUserId] [nvarchar](450) NULL,
	[gender] [nchar](2) NULL,
 CONSTRAINT [PK_Nurses] PRIMARY KEY CLUSTERED 
(
	[nurse_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vaccines]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vaccines](
	[vaccination_id] [int] IDENTITY(9700000,1) NOT NULL,
	[patient_id] [int] NOT NULL,
	[provider_id] [int] NULL,
	[vaccine_name] [varchar](50) NOT NULL,
	[date_administered] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[vaccination_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VaccinationView]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VaccinationView] AS
SELECT
    'Nurse' AS ProviderType,
    v.vaccination_id,
    v.patient_id,
    n.nurse_id AS provider_id,
    v.vaccine_name,
    v.date_administered,
    CONCAT(p.[first_name], ' ', p.[middle_initial], ' ', p.[last_name]) AS PatientName,
    CONCAT(n.[first_name], ' ', n.[middle_initial], ' ', n.[last_name]) AS ProviderName
FROM
    Vaccines v
JOIN
    Nurses n ON v.provider_id = n.nurse_id
JOIN
    Patients p ON v.patient_id = p.patient_id

UNION

SELECT
    'Doctor' AS ProviderType,
    v.vaccination_id,
    v.patient_id,
    d.doctor_id AS provider_id,
    v.vaccine_name,
    v.date_administered,
    CONCAT(p.[first_name], ' ', p.[middle_initial], ' ', p.[last_name]) AS PatientName,
    CONCAT(d.[first_name], ' ', d.[middle_initial], ' ', d.[last_name]) AS ProviderName
FROM
    Vaccines v
JOIN
    Doctors d ON v.provider_id = d.doctor_id
JOIN
    Patients p ON v.patient_id = p.patient_id;
GO
/****** Object:  View [dbo].[past_appointments]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[past_appointments] AS
SELECT
    a.appointment_id,
    CONCAT(d.last_name, ', ', d.first_name, ' ', d.middle_initial) AS DoctorName,
    CONCAT(p.last_name, ', ', p.first_name, ' ', p.middle_initial) AS PatientName,
    a.date_appointment,
    d.doctor_id, p.patient_id, d.email AS DoctorEmail, p.email AS PatientEmail
FROM
    Appointments a
    INNER JOIN Doctors d ON a.doctor_id = d.doctor_id
    INNER JOIN Patients p ON a.patient_id = p.patient_id
WHERE
    a.date_appointment < CURRENT_TIMESTAMP;
GO
/****** Object:  Table [dbo].[AppointmentHealthInformation]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppointmentHealthInformation](
	[appointment_id] [int] NOT NULL,
	[weight_lbs] [decimal](5, 2) NULL,
	[height_inches] [int] NULL,
	[heart_rate] [int] NULL,
	[systolic_pressure] [int] NULL,
	[diastolic_pressure] [int] NULL,
	[temperature_fahrenheit] [decimal](4, 2) NULL,
	[smoke_or_vape] [bit] NULL,
	[consume_alcohol] [bit] NULL,
	[allergies] [bit] NULL,
	[nurse_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[appointment_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[combined_health_view]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[combined_health_view] AS
SELECT
    ahi.appointment_id,
    ahi.weight_lbs,
    ahi.height_inches,
    ahi.heart_rate,
    ahi.systolic_pressure,
    ahi.diastolic_pressure,
    ahi.temperature_fahrenheit,
    ahi.smoke_or_vape,
    ahi.consume_alcohol,
    ahi.allergies,
    ahi.nurse_id,
	CONCAT(n.last_name, ', ', n.first_name, ' ', n.middle_initial) AS NurseName,
    p.patient_id,
    CONCAT(p.last_name, ', ', p.first_name, ' ', p.middle_initial) AS PatientName
FROM
    [dbo].[AppointmentHealthInformation] ahi
    INNER JOIN [dbo].[Appointments] a ON ahi.appointment_id = a.appointment_id
    INNER JOIN [dbo].[Patients] p ON a.patient_id = p.patient_id
	INNER JOIN [dbo].[Nurses] n ON ahi.nurse_id = n.nurse_id;
GO
/****** Object:  View [dbo].[appointment_SV]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[appointment_SV] AS
SELECT
    a.appointment_id,
    CONCAT(d.last_name, ', ', d.first_name, ' ', d.middle_initial) AS DoctorName,
    CONCAT(p.last_name, ', ', p.first_name, ' ', p.middle_initial) AS PatientName,
    a.date_appointment,
	d.doctor_id, p.patient_id, d.email AS DoctorEmail, p.email AS PatientEmail
FROM
    Appointments a
    INNER JOIN Doctors d ON a.doctor_id = d.doctor_id
    INNER JOIN Patients p ON a.patient_id = p.patient_id;
GO
/****** Object:  View [dbo].[Provider_Info]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Provider_Info] AS
SELECT
    [nurse_id] AS ID,
    [first_name] + ' ' + [middle_initial] + ' ' + [last_name] AS [FullName],
    [office] AS [Office],
    [phone] AS [PhoneNumber],
    [email] AS [Email],
    'Nurse' AS [Classification]
FROM
    [dbo].[Nurses]

UNION

SELECT
    [doctor_id] AS ID,
    [first_name] + ' ' + [middle_initial] + ' ' + [last_name] + ', MD' AS [FullName],
    [office] AS [Office],
    [phone] AS [Phone Number],
    [email] AS [Email],
    [classification] AS [Classification]
FROM
    [dbo].[Doctors] D
INNER JOIN
    [dbo].[Specialities] S ON D.[speciality_id] = S.[speciality_id];
GO
/****** Object:  Table [dbo].[Tests]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tests](
	[test_id] [int] IDENTITY(9600000,1) NOT NULL,
	[patient_id] [int] NOT NULL,
	[doctor_id] [int] NOT NULL,
	[date_test] [datetime] NOT NULL,
	[status] [varchar](10) NULL,
	[description] [nchar](100) NULL,
	[urgent] [bit] NULL,
 CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
(
	[test_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TestDetails]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Create a view that combines Tests, Doctors, and Patients information
CREATE VIEW [dbo].[TestDetails] AS
SELECT
    T.test_id,
    T.patient_id,
    P.email AS PatientEmail,
    CONCAT(p.last_name, ', ', p.first_name, ' ', p.middle_initial) AS PatientName,
    T.doctor_id,
    D.email AS DoctorEmail,
    CONCAT(d.last_name, ', ', d.first_name, ' ', d.middle_initial) AS DoctorName,
    T.date_test,
    COALESCE(T.status, 'waiting') AS status,
    T.description
FROM
    Tests T
JOIN
    Patients P ON T.patient_id = P.patient_id
JOIN
    Doctors D ON T.doctor_id = D.doctor_id;
GO
/****** Object:  View [dbo].[MostRecentHealthInfo]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MostRecentHealthInfo] AS
WITH RankedHealthInfo AS (
    SELECT
        ahi.appointment_id,
        ahi.weight_lbs,
        ahi.height_inches,
        ahi.heart_rate,
        ahi.systolic_pressure,
        ahi.diastolic_pressure,
        ahi.temperature_fahrenheit,
        ahi.smoke_or_vape,
        ahi.consume_alcohol,
        ahi.allergies,
        ahi.nurse_id,
        app.date_appointment,
        p.gender,
		p.patient_id,
        DATEDIFF(YEAR, p.DoB, GETDATE()) AS Age,
        ROW_NUMBER() OVER (PARTITION BY p.patient_id ORDER BY app.date_appointment DESC) AS RowNum
    FROM
        dbo.AppointmentHealthInformation ahi
    INNER JOIN
        dbo.Appointments app ON ahi.appointment_id = app.appointment_id
    INNER JOIN
        dbo.Patients p ON app.patient_id = p.patient_id
)
SELECT
    appointment_id,
    weight_lbs,
    height_inches,
    heart_rate,
    systolic_pressure,
    diastolic_pressure,
    temperature_fahrenheit,
    smoke_or_vape,
    consume_alcohol,
    allergies,
    nurse_id,
    gender,
    Age,
	patient_id
FROM
    RankedHealthInfo
WHERE
    RowNum = 1;
GO
/****** Object:  View [dbo].[PatientPrescriptionView]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PatientPrescriptionView] AS
SELECT
    P.patient_id,
    CONCAT(p.last_name, ', ', p.first_name, ' ', p.middle_initial) AS patient_name,
    DATEDIFF(YEAR, p.DoB, GETDATE()) AS age,
    P.gender,
    PR.drug_name,
    PR.dosage,
    PR.date_prescription
FROM
    dbo.Patients AS P, dbo.Prescriptions AS PR
WHERE P.patient_id = PR.patient_id;
GO
/****** Object:  View [dbo].[Our_Providers]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Our_Providers] AS
SELECT
    [nurse_id] AS ID,
    [first_name] + ' ' + [last_name] AS [FullName],
    [office] AS [Office],
    [phone] AS [PhoneNumber],
    [email] AS [Email],
    'Nurse' AS [Classification],
    [gender] as [gender]
FROM
    [dbo].[Nurses]

UNION

SELECT
    [doctor_id] AS ID,
    [first_name] + ' ' + [last_name] AS [FullName],
    [office] AS [Office],
    [phone] AS [PhoneNumber],
    [email] AS [Email],
    [classification] AS [Classification],
    [gender] as [gender]
FROM
    [dbo].[Doctors] D
INNER JOIN
    [dbo].[Specialities] S ON D.[speciality_id] = S.[speciality_id];
GO
/****** Object:  View [dbo].[PatientAppointment]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREATE VIEW DoctorsPatientEmergencyContactList AS
--SELECT
 --  d.last_name AS DoctorLastName,
  -- p.last_name AS PatientLastName,
   --p.first_name AS PatientFirstName,
   --p.patient_id,
   --e.emergency_contact_id,
  -- e.phone,
  -- e.last_name AS EmergencyContactLastName,
  -- e.first_name AS EmergencyContactFirstName
--FROM
   --dbo.EmergencyContacts AS e,
  -- dbo.Patients AS p,
 --  dbo.Doctors AS d;

CREATE VIEW [dbo].[PatientAppointment] AS
SELECT 
   a.appointment_id as AppointmentId,
   p.patient_id,
   p.[last_name] + ', ' + p.[first_name] AS PatientLastName,
   d.doctor_id as DoctorId,
   d.[last_name] + ', ' + d.[first_name] AS DoctorLastName,
   d.office AS DoctorOffice,
   p.email AS PatientEmail,
   a.date_appointment AS AppointmentDate

FROM

dbo.Doctors AS d,
dbo.Patients AS p,
dbo.Appointments as a;
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[admin_id] [int] IDENTITY(8000,1) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[middle_initial] [varchar](50) NULL,
	[last_name] [varchar](50) NOT NULL,
	[office] [varchar](255) NULL,
	[DoB] [date] NOT NULL,
	[phone] [varchar](13) NOT NULL,
	[email] [varchar](255) NOT NULL,
	[IdentityUserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED 
(
	[admin_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmergencyContacts]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmergencyContacts](
	[emergency_contact_id] [int] IDENTITY(4110000,1) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[middle_initial] [varchar](50) NULL,
	[last_name] [varchar](50) NOT NULL,
	[phone] [varchar](13) NOT NULL,
	[email] [varchar](255) NOT NULL,
	[patient_id] [int] NOT NULL,
	[relation] [varchar](100) NOT NULL,
 CONSTRAINT [PK_EmergencyContacts] PRIMARY KEY CLUSTERED 
(
	[emergency_contact_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IdentityRole<string>]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityRole<string>](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[NormalizedName] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_IdentityRole<string>] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalHistories]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalHistories](
	[medical_history_id] [int] IDENTITY(1,1) NOT NULL,
	[diagnosis_info] [text] NULL,
	[surgeries] [text] NULL,
	[medication] [text] NULL,
	[allergies] [text] NULL,
	[patient_id] [int] NOT NULL,
 CONSTRAINT [PK_MedicalHistories] PRIMARY KEY CLUSTERED 
(
	[medical_history_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MetaDatas]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MetaDatas](
	[meta_data_id] [int] IDENTITY(1,1) NOT NULL,
	[login_info] [text] NOT NULL,
	[changelog] [text] NOT NULL,
	[identifier_number] [varchar](100) NOT NULL,
 CONSTRAINT [PK_MetaDatas] PRIMARY KEY CLUSTERED 
(
	[meta_data_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[schedule_id] [int] IDENTITY(500,1) NOT NULL,
	[doctor_id] [int] NOT NULL,
	[date_schedule] [date] NOT NULL,
	[start_time] [time](7) NOT NULL,
	[end_time] [time](7) NOT NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[schedule_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Admins_IdentityUserId]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Admins_IdentityUserId] ON [dbo].[Admins]
(
	[IdentityUserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Appointments_doctor_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Appointments_doctor_id] ON [dbo].[Appointments]
(
	[doctor_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Appointments_patient_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Appointments_patient_id] ON [dbo].[Appointments]
(
	[patient_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Doctors_IdentityUserId]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Doctors_IdentityUserId] ON [dbo].[Doctors]
(
	[IdentityUserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Doctors_speciality_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Doctors_speciality_id] ON [dbo].[Doctors]
(
	[speciality_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmergencyContacts_patient_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmergencyContacts_patient_id] ON [dbo].[EmergencyContacts]
(
	[patient_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Insurances_patient_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Insurances_patient_id] ON [dbo].[Insurances]
(
	[patient_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Invoices_insurance_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Invoices_insurance_id] ON [dbo].[Invoices]
(
	[insurance_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Invoices_patient_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Invoices_patient_id] ON [dbo].[Invoices]
(
	[patient_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_MedicalHistories_patient_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_MedicalHistories_patient_id] ON [dbo].[MedicalHistories]
(
	[patient_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Nurses_IdentityUserId]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Nurses_IdentityUserId] ON [dbo].[Nurses]
(
	[IdentityUserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Patients_IdentityUserId]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Patients_IdentityUserId] ON [dbo].[Patients]
(
	[IdentityUserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Prescriptions_doctor_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Prescriptions_doctor_id] ON [dbo].[Prescriptions]
(
	[doctor_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Prescriptions_patient_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Prescriptions_patient_id] ON [dbo].[Prescriptions]
(
	[patient_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Referrals_patient_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Referrals_patient_id] ON [dbo].[Referrals]
(
	[patient_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Referrals_primary_doctor_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Referrals_primary_doctor_id] ON [dbo].[Referrals]
(
	[primary_doctor_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Referrals_specialist_doctor_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Referrals_specialist_doctor_id] ON [dbo].[Referrals]
(
	[specialist_doctor_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Referrals_speciality_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Referrals_speciality_id] ON [dbo].[Referrals]
(
	[speciality_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Schedules_doctor_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Schedules_doctor_id] ON [dbo].[Schedules]
(
	[doctor_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tests_doctor_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Tests_doctor_id] ON [dbo].[Tests]
(
	[doctor_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tests_patient_id]    Script Date: 11/25/2023 11:17:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_Tests_patient_id] ON [dbo].[Tests]
(
	[patient_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Notifications] ADD  DEFAULT (((sysdatetimeoffset() AT TIME ZONE 'UTC') AT TIME ZONE 'Central Standard Time')) FOR [created_at]
GO
ALTER TABLE [dbo].[Notifications] ADD  DEFAULT ((0)) FOR [is_read]
GO
ALTER TABLE [dbo].[Tests] ADD  DEFAULT ((0)) FOR [urgent]
GO
ALTER TABLE [dbo].[Admins]  WITH CHECK ADD  CONSTRAINT [FK_Admins_AspNetUsers_IdentityUserId] FOREIGN KEY([IdentityUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Admins] CHECK CONSTRAINT [FK_Admins_AspNetUsers_IdentityUserId]
GO
ALTER TABLE [dbo].[AppointmentHealthInformation]  WITH CHECK ADD FOREIGN KEY([appointment_id])
REFERENCES [dbo].[Appointments] ([appointment_id])
GO
ALTER TABLE [dbo].[AppointmentHealthInformation]  WITH CHECK ADD FOREIGN KEY([nurse_id])
REFERENCES [dbo].[Nurses] ([nurse_id])
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK__Appointme__docto__778AC167] FOREIGN KEY([doctor_id])
REFERENCES [dbo].[Doctors] ([doctor_id])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK__Appointme__docto__778AC167]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK__Appointme__patie__76969D2E] FOREIGN KEY([patient_id])
REFERENCES [dbo].[Patients] ([patient_id])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK__Appointme__patie__76969D2E]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Doctors]  WITH CHECK ADD  CONSTRAINT [FK__Doctors__special__5EBF139D] FOREIGN KEY([speciality_id])
REFERENCES [dbo].[Specialities] ([speciality_id])
GO
ALTER TABLE [dbo].[Doctors] CHECK CONSTRAINT [FK__Doctors__special__5EBF139D]
GO
ALTER TABLE [dbo].[Doctors]  WITH CHECK ADD  CONSTRAINT [FK_Doctors_AspNetUsers_IdentityUserId] FOREIGN KEY([IdentityUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Doctors] CHECK CONSTRAINT [FK_Doctors_AspNetUsers_IdentityUserId]
GO
ALTER TABLE [dbo].[EmergencyContacts]  WITH CHECK ADD  CONSTRAINT [FK__Emergency__patie__6E01572D] FOREIGN KEY([patient_id])
REFERENCES [dbo].[Patients] ([patient_id])
GO
ALTER TABLE [dbo].[EmergencyContacts] CHECK CONSTRAINT [FK__Emergency__patie__6E01572D]
GO
ALTER TABLE [dbo].[Insurances]  WITH CHECK ADD  CONSTRAINT [FK__Insurance__patie__68487DD7] FOREIGN KEY([patient_id])
REFERENCES [dbo].[Patients] ([patient_id])
GO
ALTER TABLE [dbo].[Insurances] CHECK CONSTRAINT [FK__Insurance__patie__68487DD7]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK__Invoices__insura__06CD04F7] FOREIGN KEY([insurance_id])
REFERENCES [dbo].[Insurances] ([insurance_id])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK__Invoices__insura__06CD04F7]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK__Invoices__patien__05D8E0BE] FOREIGN KEY([patient_id])
REFERENCES [dbo].[Patients] ([patient_id])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK__Invoices__patien__05D8E0BE]
GO
ALTER TABLE [dbo].[MedicalHistories]  WITH CHECK ADD  CONSTRAINT [FK__MedicalHi__patie__6B24EA82] FOREIGN KEY([patient_id])
REFERENCES [dbo].[Patients] ([patient_id])
GO
ALTER TABLE [dbo].[MedicalHistories] CHECK CONSTRAINT [FK__MedicalHi__patie__6B24EA82]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD FOREIGN KEY([patient_id])
REFERENCES [dbo].[Patients] ([patient_id])
GO
ALTER TABLE [dbo].[Nurses]  WITH CHECK ADD  CONSTRAINT [FK_Nurses_AspNetUsers_IdentityUserId] FOREIGN KEY([IdentityUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Nurses] CHECK CONSTRAINT [FK_Nurses_AspNetUsers_IdentityUserId]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_AspNetUsers_IdentityUserId] FOREIGN KEY([IdentityUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_AspNetUsers_IdentityUserId]
GO
ALTER TABLE [dbo].[Prescriptions]  WITH CHECK ADD  CONSTRAINT [FK__Prescript__docto__7F2BE32F] FOREIGN KEY([doctor_id])
REFERENCES [dbo].[Doctors] ([doctor_id])
GO
ALTER TABLE [dbo].[Prescriptions] CHECK CONSTRAINT [FK__Prescript__docto__7F2BE32F]
GO
ALTER TABLE [dbo].[Prescriptions]  WITH CHECK ADD  CONSTRAINT [FK__Prescript__patie__00200768] FOREIGN KEY([patient_id])
REFERENCES [dbo].[Patients] ([patient_id])
GO
ALTER TABLE [dbo].[Prescriptions] CHECK CONSTRAINT [FK__Prescript__patie__00200768]
GO
ALTER TABLE [dbo].[Referrals]  WITH CHECK ADD  CONSTRAINT [FK__Referrals__patie__73BA3083] FOREIGN KEY([patient_id])
REFERENCES [dbo].[Patients] ([patient_id])
GO
ALTER TABLE [dbo].[Referrals] CHECK CONSTRAINT [FK__Referrals__patie__73BA3083]
GO
ALTER TABLE [dbo].[Referrals]  WITH CHECK ADD  CONSTRAINT [FK__Referrals__prima__70DDC3D8] FOREIGN KEY([primary_doctor_id])
REFERENCES [dbo].[Doctors] ([doctor_id])
GO
ALTER TABLE [dbo].[Referrals] CHECK CONSTRAINT [FK__Referrals__prima__70DDC3D8]
GO
ALTER TABLE [dbo].[Referrals]  WITH CHECK ADD  CONSTRAINT [FK__Referrals__speci__71D1E811] FOREIGN KEY([specialist_doctor_id])
REFERENCES [dbo].[Doctors] ([doctor_id])
GO
ALTER TABLE [dbo].[Referrals] CHECK CONSTRAINT [FK__Referrals__speci__71D1E811]
GO
ALTER TABLE [dbo].[Referrals]  WITH CHECK ADD  CONSTRAINT [FK__Referrals__speci__72C60C4A] FOREIGN KEY([speciality_id])
REFERENCES [dbo].[Specialities] ([speciality_id])
GO
ALTER TABLE [dbo].[Referrals] CHECK CONSTRAINT [FK__Referrals__speci__72C60C4A]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK__Schedules__docto__02FC7413] FOREIGN KEY([doctor_id])
REFERENCES [dbo].[Doctors] ([doctor_id])
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK__Schedules__docto__02FC7413]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK__Tests__doctor_id__7C4F7684] FOREIGN KEY([doctor_id])
REFERENCES [dbo].[Doctors] ([doctor_id])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK__Tests__doctor_id__7C4F7684]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK__Tests__patient_i__7B5B524B] FOREIGN KEY([patient_id])
REFERENCES [dbo].[Patients] ([patient_id])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK__Tests__patient_i__7B5B524B]
GO
ALTER TABLE [dbo].[Vaccines]  WITH CHECK ADD FOREIGN KEY([patient_id])
REFERENCES [dbo].[Patients] ([patient_id])
GO
ALTER TABLE [dbo].[Vaccines]  WITH CHECK ADD  CONSTRAINT [check_nurse_doc_null] CHECK  (([provider_id] IS NOT NULL))
GO
ALTER TABLE [dbo].[Vaccines] CHECK CONSTRAINT [check_nurse_doc_null]
GO
/****** Object:  StoredProcedure [dbo].[ThrowAppointmentOverlapError]    Script Date: 11/25/2023 11:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ThrowAppointmentOverlapError]
AS
BEGIN
    -- You can customize the error message and severity to fit your needs
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;

    SELECT 
        @ErrorMessage = 'There is already an exist appointment for this doctor that conflicts with this time. Please choose a different time.',
        @ErrorSeverity = 16,
        @ErrorState = 1;

    -- This will throw the error
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END;
GO
ALTER DATABASE [team12Main] SET  READ_WRITE 
GO
