using Otp.Service.Interfaces;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Otp.Service.Implementation;

public class OtpService : IOtpService
{
    private readonly ConcurrentDictionary<string, (string otp, DateTime expiry)> _otpStorage;
    private const int OTP_LENGTH = 6;
    private const int OTP_EXPIRY_SECONDS = 60;
    private const string ALLOWED_DOMAIN = "@dso.org.sg";

    public OtpService()
    {
        _otpStorage = new ConcurrentDictionary<string, (string, DateTime)>();
    }

    public string GenerateOtp()
    {
        using var rng = new RNGCryptoServiceProvider();
        var bytes = new byte[OTP_LENGTH];
        rng.GetBytes(bytes);
        return BitConverter.ToString(bytes).Replace("-", "").Substring(0, OTP_LENGTH);
    }

    public bool ValidateEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@dso\.org\.sg$", RegexOptions.IgnoreCase);
    }

    public bool StoreOtp(string email, string otp)
    {
        return _otpStorage.TryAdd(email, (otp, DateTime.UtcNow.AddSeconds(OTP_EXPIRY_SECONDS)));
    }

    public string GetStoredOtp(string email)
    {
        return _otpStorage.TryGetValue(email, out var otpData) && otpData.expiry > DateTime.UtcNow ? otpData.otp : null;
    }

    public void RemoveOtp(string email)
    {
        _otpStorage.TryRemove(email, out _);
    }
}
