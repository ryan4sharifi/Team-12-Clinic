using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using med_test8.Data;
using med_test8.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;
using System.Net;
using System.Net.Mail;


public class EmailService
{
    public void SendEmail(string to, string subject, string body)
    {
        using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
        {
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("team12database@gmail.com", "vtzk ushh ggzb yeil");
            smtpClient.EnableSsl = true;
            smtpClient.Port = 587;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("team12database@gmail.com");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;

            smtpClient.Send(mail);
        }
    }
}

/*
private async Task SendAppointmentConfirmationEmail(Appointments appointment)
{
    // Replace these with your actual email settings
    var smtpClient = new SmtpClient("smtp.gmail.com", 587)
    {
        Port = 587,
        Credentials = new NetworkCredential("team12database@gmail.com", "vtzk ushh ggzb yeil"),
        EnableSsl = true,
    };

    var patient = _context.Patients.Find(appointment.patient_id);

    var mailMessage = new MailMessage
    {
        From = new MailAddress("team12database@gmail.com"),
        Subject = "Appointment Confirmation",
        Body = $"Hello, {patient.first_name}. Your appointment on {appointment.date_appointment} has been successfully scheduled."
    };

    mailMessage.To.Add(patient.email); // Replace with the actual customer's email address

    await smtpClient.SendMailAsync(mailMessage);
}
*/