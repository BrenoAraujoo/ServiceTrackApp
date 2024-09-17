using System.ComponentModel.DataAnnotations;
using ServiceTrackHub.Domain.Enums.ValueObjects;

namespace ServiceTrackHub.Application.InputViewModel.Auth;

public record LoginModel
{
    public string Email { get; private set; }
    public string Password { get; private set; }

    public LoginModel(string email, string password)
    {
        Email = new Email(email).Value;
        Password = new Password(password).Value;
    }
}
