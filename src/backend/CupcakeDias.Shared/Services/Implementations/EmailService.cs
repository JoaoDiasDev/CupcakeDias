using CupcakeDias.Shared.Services.Interfaces;
using dotenv.net;
using MailKit.Net.Smtp;
using MimeKit;

namespace CupcakeDias.Shared.Services.Implementations;

public class EmailService : IEmailService
{
    private const string SmtpServer = "smtp.gmail.com";
    private const int SmtpPort = 587;
    private const string SenderEmail = "joaodiasworking@gmail.com";
    private readonly string _senderPassword = DotEnv.Read()["SMTP_PASSWORD"];

    public async Task SendEmailAsync(string userEmail, string subject, string message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("CupcakeDias", SenderEmail));
        emailMessage.To.Add(new MailboxAddress("", userEmail));
        emailMessage.Subject = subject;

        emailMessage.Body = new TextPart("plain")
        {
            Text = message
        };

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(SmtpServer, SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(SenderEmail, _senderPassword);

            await client.SendAsync(emailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            throw;
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}
