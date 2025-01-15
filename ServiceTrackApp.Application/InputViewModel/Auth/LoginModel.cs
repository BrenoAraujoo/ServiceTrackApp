namespace ServiceTrackHub.Application.InputViewModel.Auth;

public record LoginModel(string Email, string Password)
{
    public string Email { get; private set; } = Email;
    public string Password { get; private set; } = Password;
}
