using BharatMedicsV2.Models.DTOs;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;


namespace BharatMedicsV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        [HttpPost("Send Email")]

        public IActionResult SendEmail(EmailDTO emailDTO)
        {
            var eml = new MimeMessage();
            eml.From.Add(MailboxAddress.Parse("bhratmedics@gmail.com"));
            eml.To.Add(MailboxAddress.Parse(emailDTO.Email));

            eml.Subject = "Order Received Confirmation Mail";
            eml.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailDTO.Subject};
            using var smtp = new SmtpClient();

            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("pharamacare@gmail.com", "ykocyltjuhioyunl");

            smtp.Send(eml);
            smtp.Disconnect(true);
            return Ok();
        }
    }
}
