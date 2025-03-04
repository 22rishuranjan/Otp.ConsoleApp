using Otp.Service.Interfaces;
using Otp.Service;
using System.Diagnostics;

namespace Otp.ConsoleApp;
public class OtpWorkflow
{
    private readonly IOtpService _otpService;
    private readonly IEmailService _emailService;
    private const int MaxAttempts = 10;
    private const int TimeoutSeconds = 60;

    public OtpWorkflow(IOtpService otpService, IEmailService emailService)
    {
        _otpService = otpService;
        _emailService = emailService;
    }

    public bool GenerateAndSendOtp(string userEmail)
    {
        if (!_otpService.ValidateEmail(userEmail))
        {
            Console.WriteLine("Invalid email. Only @dso.org.sg emails are allowed.");
            return false;
        }

        string otp = _otpService.GenerateOtp();
        _otpService.StoreOtp(userEmail, otp);

        string emailBody = $"Your Otp Code is {otp}. The code is valid for 1 minute.";
        bool emailSent = _emailService.SendEmail(userEmail, emailBody);

        Console.WriteLine(emailSent ? "Otp sent successfully!" : "Failed to send Otp.");
        return emailSent;
    }

    public int CheckOtp(string userEmail, Func<string> inputReader)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        int attempts = 0;

        while (attempts < MaxAttempts && stopwatch.Elapsed.TotalSeconds < TimeoutSeconds)
        {
            Console.Write("Enter Otp: ");
            string inputOtp = inputReader.Invoke();
            string storedOtp = _otpService.GetStoredOtp(userEmail);

            if (storedOtp == null) return StatusCodes.STATUS_OTP_TIMEOUT;
            if (inputOtp == storedOtp)
            {
                Console.WriteLine("Otp validated successfully!");
                _otpService.RemoveOtp(userEmail);
                return StatusCodes.STATUS_OTP_OK;
            }

            attempts++;
            Console.WriteLine($"Incorrect Otp. {MaxAttempts - attempts} attempts left.");
        }

        return attempts >= MaxAttempts ? StatusCodes.STATUS_OTP_FAIL : StatusCodes.STATUS_OTP_TIMEOUT;
    }
}
