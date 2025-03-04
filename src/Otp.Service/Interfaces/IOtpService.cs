namespace Otp.Service.Interfaces;  

public interface IOtpService
{
    string GenerateOtp();
    bool ValidateEmail(string email);
    bool StoreOtp(string email, string otp);
    string GetStoredOtp(string email);
    void RemoveOtp(string email);
}
