using ServiceTrackApp.Domain.Common.Erros;
using ServiceTrackApp.Domain.Enums;
using ServiceTrackApp.Domain.ValueObjects;

namespace ServiceTrackApp.Application.InputViewModel.User
{
    public record CreateUserModel(

        string Name,
        string Email,
        string SmartPhoneNumber,
        string Password,
        string? JobPosition,
        string Role
    );

}
