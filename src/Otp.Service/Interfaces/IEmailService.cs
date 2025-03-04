namespace Otp.Service.Interfaces;

public interface IEmailService
{
    bool SendEmail(string toEmail, string body);
}
