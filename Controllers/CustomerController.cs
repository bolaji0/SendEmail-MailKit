using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendEmail_MailKit.Models;
using SendEmail_MailKit.Models.DTO;
using SendEmail_MailKit.Service;

namespace SendEmail_MailKit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public CustomerController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        
        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail(MailRequestDTO mail)
        {
            try
            {
                MailRequest mailRequest = new()
                {
                    ToEmail = mail.ToEmail,
                    Subject = mail.Subject,
                    Body = sendHTML()
                };

                await _emailService.SendEmailAsync(mailRequest);

                return Ok();
            }
            catch (Exception ex)
            {
                // Return a BadRequest response with a message to the user.
                return BadRequest(ex.Message);
            }
        }

        //This method is used to pass html and our styling or a link to our email
        private string sendHTML()
        {
            string html = "<h1 style=\"color:red;\">Testing the email</h1>";
            return html;
        }
    }
}
