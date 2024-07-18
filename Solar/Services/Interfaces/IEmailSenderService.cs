using Solar.Models.Static;

namespace Solar.Services.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(EmailClient sender, EmailClient reciver, string subject, string message);
    }
}
