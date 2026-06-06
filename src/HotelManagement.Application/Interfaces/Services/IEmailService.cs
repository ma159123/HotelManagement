namespace HotelManagement.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(
    string toEmail,
    string userName,
    string confirmationLink,
    CancellationToken cancellationToken = default);

        Task SendPasswordResetEmailAsync(
            string toEmail,
            string userName,
            string resetLink,
            CancellationToken cancellationToken = default);

    }
}
