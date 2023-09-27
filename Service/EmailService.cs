using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SendEmail_MailKit.Models;
//using System.Net.Mail;

namespace SendEmail_MailKit.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
           this.emailSettings = options.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(emailSettings.Email);   
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
          
            //This is to send any attachement 
            byte[] fileBytes;
            if (System.IO.File.Exists("Attachment/image.JPG"))
            {
                FileStream file = new FileStream("Attachment/image.JPG", FileMode.Open, FileAccess.Read);
                using (var sr = new MemoryStream())
                {
                    file.CopyTo(sr);
                    fileBytes= sr.ToArray();
                }
                builder.Attachments.Add("Attachment.JPG", fileBytes, ContentType.Parse("application/octet-stream"));
            }


            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailSettings.Email, emailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
