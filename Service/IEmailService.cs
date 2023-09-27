using SendEmail_MailKit.Models;

namespace SendEmail_MailKit.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
