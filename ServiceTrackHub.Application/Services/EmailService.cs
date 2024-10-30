using ServiceTrackHub.Application.Interfaces.Email;

namespace ServiceTrackHub.Application.Services;

public class EmailService : IEmailService
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        throw new NotImplementedException();
    }
}