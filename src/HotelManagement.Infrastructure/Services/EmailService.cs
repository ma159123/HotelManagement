using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Application.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace HotelManagement.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
            => _settings = settings.Value;
        public async Task SendConfirmationEmailAsync(
        string toEmail,
        string userName,
        string confirmationLink,
        CancellationToken cancellationToken = default)
        {
            var subject = "Confirm Your Email — Hotel Management";

            // HTML body contain Link
            var body = $@"
            <h2>Welcome, {userName}!</h2>
            <p>Please confirm your email by clicking the link below:</p>
            <a href='{confirmationLink}'>Confirm Email</a>
            <p>This link will expire in 24 hours.</p>
        ";

            await SendEmailAsync(toEmail, subject, body, cancellationToken);
        }

        // ─── إيميل Reset Password ─────────
        public async Task SendPasswordResetEmailAsync(string toEmail,
            string userName,
            string resetLink,
            CancellationToken cancellationToken = default)
        {
            // نفس الفكرة بس لـ Password Reset
        }

        // ─── Core Method ───────────────────
        private async Task SendEmailAsync(
            string toEmail,
            string subject,
            string htmlBody,
            CancellationToken cancellationToken)
        {
            // 1. اعمل الـ Message
            var message = new MimeMessage();

            // From: مين بيبعت
            message.From.Add(new MailboxAddress(
                _settings.SenderName,
                _settings.SenderEmail));

            // To: مين يستلم
            message.To.Add(MailboxAddress.Parse(toEmail));

            // Subject
            message.Subject = subject;

            // Body: HTML
            message.Body = new TextPart("html") { Text = htmlBody };

            // 2. Connect لـ SMTP Server
            using var client = new SmtpClient();

            await client.ConnectAsync(
                _settings.Host,           // smtp.gmail.com
                _settings.Port,           // 587
                SecureSocketOptions.StartTls,  // Encryption
                cancellationToken);

            // 3. Login
            await client.AuthenticateAsync(
                _settings.SenderEmail,
                _settings.Password,
                cancellationToken);

            // 4. Send
            await client.SendAsync(message, cancellationToken);

            // 5. Disconnect
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}
