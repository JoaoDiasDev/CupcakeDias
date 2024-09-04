using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using CupcakeDias.Shared.Services.Interfaces;
using dotenv.net;

namespace CupcakeDias.Shared.Services.Implementations;

public class EmailService : IEmailService
{
    private readonly string smtpServer = "smtp.gmail.com";
    private readonly int smtpPort = 587;
    private readonly string senderEmail = "joaodiasworking@gmail.com";
    private readonly string senderPassword = DotEnv.Read()["SMTP_PASSWORD"] ?? string.Empty;

    public async Task SendEmailAsync(string userEmail, string subject, string message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("CupcakeDias", senderEmail));
        emailMessage.To.Add(new MailboxAddress("", userEmail));
        emailMessage.Subject = subject;

        emailMessage.Body = new TextPart("plain")
        {
            Text = message
        };

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(senderEmail, senderPassword);

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
            client.Dispose();
        }
    }
}
