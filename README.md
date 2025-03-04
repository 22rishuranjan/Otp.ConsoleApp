# Otp Console Application

## Overview
The Otp Console Application is a .NET Core project that generates One-Time Passwords (Otp), validates emails, and sends Otp codes via email using SMTP. It follows dependency injection (DI) principles for loose coupling and modular design.

## Project Structure
```
OtpConsoleApp/
│-- solution/             (Solution folder)
│-- src/                  (Source folder containing projects)
│   │-- Otp.Service/    (Class Library - Handles Otp logic)
│   │   │-- Interfaces/   (Dependency Injection Interfaces)
│   │   │-- Services/     (Service Implementations)
│   │-- Otp.ConsoleApp/    (Console App using the Otp Service)
│   │   │-- Program.cs    (Entry Point - sets up DI)
│   │   │-- OtpWorkflow.cs (Handles generic Otp steps)
│   │   │-- UserOtpHandler.cs (Handles user-specific logic)
│-- appsettings.json      (Configuration for SMTP)
│-- README.md             (Project Documentation)
```

## Features
- Otp Generation & Validation  
- SMTP Email Sending  
- Configurable via `appsettings.json`  
- Dependency Injection (DI) for Loose Coupling  
- Retry & Timeout for Otp Entry  
- Supports Local SMTP Testing with Papercut  

## Setup & Installation

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed
- An SMTP server (Gmail, Outlook, or a custom SMTP provider)
- Ensure `appsettings.json` is correctly placed in the project
- Optional: [Papercut SMTP](https://github.com/ChangemakerStudios/Papercut-SMTP) for local email testing

### Clone the Repository
```sh
git clone https://github.com/your-repo/OtpConsoleApp.git
cd OtpConsoleApp
```

### Build the Project
```sh
dotnet build
```

### Run the Application
```sh
dotnet run --project OtpConsoleApp
```

## Configuration (`appsettings.json`)

Modify the SMTP settings in `appsettings.json`:
```json
{
  "SMTP": {
    "Host": "localhost", // Change to "smtp.your-email-provider.com" for production
    "Port": "25", // Papercut default port (for local testing)
    "Username": "your-email@example.com",
    "Password": "your-email-password",
    "Sender": "your-email@example.com"
  }
}
```

### Using Papercut for Local Testing
- Download & Install [Papercut SMTP](https://github.com/ChangemakerStudios/Papercut-SMTP)
- Set `Host` to `localhost` and `Port` to `25` in `appsettings.json`
- Run the application and open Papercut to view incoming emails

## How to Use

1. Enter your email (must be `@dso.org.sg` domain).  
2. The application generates & sends an Otp.  
3. Enter the received Otp.  
4. If valid, it confirms "Otp validated successfully!".  
5. If incorrect, it allows up to 10 attempts before failure.  


