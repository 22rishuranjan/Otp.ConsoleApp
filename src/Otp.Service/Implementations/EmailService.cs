
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Otp.Service.Interfaces;

namespace Otp.Service;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public bool SendEmail(string toEmail, string body)
    {
        try
        {
            var smtpHost = _config["SMTP:Host"];
            bool isLocalSmtp = smtpHost.Equals("localhost", StringComparison.OrdinalIgnoreCase);

            using var smtpClient = isLocalSmtp
                ? new SmtpClient("localhost") { Port = 25, EnableSsl = false }
                : new SmtpClient(smtpHost)
                {
                    Port = int.Parse(_config["SMTP:Port"]),
                    Credentials = new NetworkCredential(_config["SMTP:Username"], _config["SMTP:Password"]),
                    EnableSsl = true
                };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config["SMTP:Sender"]),
                Subject = "Your Otp Code",
                Body = body,
                IsBodyHtml = false
            };
            mailMessage.To.Add(toEmail);

            smtpClient.Send(mailMessage);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
            return false;
        }
    }
}
