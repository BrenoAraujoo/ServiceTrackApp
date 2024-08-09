namespace ServiceTrackHub.Application.InputViewModel.User
{
    public record UpdateUserInputModel(
        string? Name,
        string? Email,
        string? Phone,
        string? Password
    );
}
