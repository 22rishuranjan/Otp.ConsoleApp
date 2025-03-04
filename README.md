# Otp Console Application

## 📚 Overview
The **Otp Console Application** is a .NET Core project that generates One-Time Passwords (Otp), validates emails, and sends Otp codes via email using SMTP. It follows **dependency injection (DI)** principles for loose coupling and modular design.

## 🏰️ Project Structure
```
OtpVerificationSystem/
│-- OtpServiceLib/       (Class Library - Handles Otp logic)
│   │-- Interfaces/      (Dependency Injection Interfaces)
│   │-- Services/        (Service Implementations)
│-- OtpConsoleApp/       (Console App using the Otp Service)
│   │-- Program.cs       (Entry Point - sets up DI)
│   │-- OtpWorkflow.cs   (Handles generic Otp steps)
│   │-- UserOtpHandler.cs (Handles user-specific logic)
│-- appsettings.json     (Configuration for SMTP)
│-- README.md            (Project Documentation)
```

---

## 📌 Features
✅ **Otp Generation & Validation**  
✅ **SMTP Email Sending**  
✅ **Configurable via `appsettings.json`**  
✅ **Dependency Injection (DI) for Loose Coupling**  
✅ **Retry & Timeout for Otp Entry**  

---

## 🔧 Setup & Installation

### **1️⃣ Prerequisites**
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed
- An SMTP server (Gmail, Outlook, or a custom SMTP provider)
- Ensure **`appsettings.json`** is correctly placed in the project

### **2️⃣ Clone the Repository**
```sh
git clone https://github.com/your-repo/OtpConsoleApp.git
cd OtpConsoleApp
```

### **3️⃣ Build the Project**
```sh
dotnet build
```

### **4️⃣ Run the Application**
```sh
dotnet run --project OtpConsoleApp
```

---

## 📁 Configuration (`appsettings.json`)

Modify the SMTP settings in `appsettings.json`:
```json
{
  "SMTP": {
    "Host": "smtp.your-email-provider.com",
    "Port": "587",
    "Username": "your-email@example.com",
    "Password": "your-email-password",
    "Sender": "your-email@example.com"
  }
}
```
---

## 🖥️ How to Use

1️⃣ **Enter your email** (must be `@dso.org.sg` domain).  
2️⃣ The application **generates & sends** an Otp.  
3️⃣ Enter the received Otp.  
4️⃣ If valid, it confirms **"Otp validated successfully!"**.  
5️⃣ If incorrect, it allows **up to 10 attempts** before failure.  



## 🧪 Running Unit Tests
Run the test cases using `xUnit`:
```sh
dot test run
