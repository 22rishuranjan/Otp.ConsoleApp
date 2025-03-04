
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Otp.ConsoleApp;

class Program
{
    static void Main()
    {
        var serviceProvider = ConfigureServices();
        var otpWorkflow = serviceProvider.GetRequiredService<OtpWorkflow>();

        Console.Write("Enter your email: ");
        string userEmail = Console.ReadLine();

        if (otpWorkflow.GenerateAndSendOtp(userEmail))
        {
            otpWorkflow.CheckOtp(userEmail, () => Console.ReadLine());
        }
    }

    private static ServiceProvider ConfigureServices()
    {
        var configBuilder = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        var config = configBuilder.Build();

        if (!config.Providers.GetEnumerator().MoveNext())
        {
            Console.WriteLine("Warning: Configuration file 'appsettings.json' not found. Using default values.");
        }

        return new ServiceCollection()
            .AddSingleton<IConfiguration>(config)
            .AddSingleton<IOtpService, OtpService>()
            .AddSingleton<IEmailService, EmailService>()
             .AddSingleton<OtpWorkflow>()
            .BuildServiceProvider();
    }
}
