

namespace ServiceTrackApp.Application.InputViewModel.User
{
    public record UpdateUserModel(
        string? Name,
        string? Email,
        string? SmartPhoneNumber,
        string? JobPosition,
        string? UserRole,
        string? Password
    );
}
