namespace ServiceTrackHub.Application.InputViewModel.User
{
    public record UpdateUserModel(
        string? Name,
        string? Email,
        string? Phone,
        string? Password
    );
}
