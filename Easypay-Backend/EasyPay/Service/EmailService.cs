using EasyPay.Service;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace EasyPay.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string messageBody)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Payroll System", _configuration["EmailSettings:FromEmail"]));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = messageBody };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(_configuration["EmailSettings:SmtpHost"], int.Parse(_configuration["EmailSettings:SmtpPort"]), true);
                client.Authenticate(_configuration["EmailSettings:SmtpUser"], _configuration["EmailSettings:SmtpPass"]);

                await client.SendAsync(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
