using System;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string userEmail, string subject, string message);
}
