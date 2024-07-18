using Solar.Models.Static;
using System.Net.Mail;
using System.Net;
using Solar.Services.Interfaces;

namespace Solar.Services.StaticServices
{
    public class EmailSenderService : IEmailSenderService
    {
        public Task SendEmailAsync(EmailClient sender, EmailClient reciver, string subject, string message)
        {

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(sender.Email, sender.Password)
            };

            return client.SendMailAsync(
                new MailMessage(from: sender.Email,
                                to: reciver.Email,
                                subject,
                                message
                ));
        }
    }
}
