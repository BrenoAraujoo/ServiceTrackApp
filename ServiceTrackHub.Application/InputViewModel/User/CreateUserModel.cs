namespace ServiceTrackHub.Application.InputViewModel.User
{
    public record CreateUserModel(

        string Name,
        string Email,
        string SmartPhoneNumber,
        string Password,
        string? JobPosition
        );
}
