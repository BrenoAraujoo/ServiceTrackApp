namespace ServiceTrackApp.Application.InputViewModel.Auth;

public record TokenRequest(
    string AccessToken,
    string RefreshToken
);