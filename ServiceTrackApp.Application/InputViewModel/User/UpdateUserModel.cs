

using ServiceTrackApp.Domain.Common.Erros;
using ServiceTrackApp.Domain.Enums;
using ServiceTrackApp.Domain.ValueObjects;

namespace ServiceTrackApp.Application.InputViewModel.User
{
    public record UpdateUserModel(
        string? Name,
        string? Email,
        string? SmartPhoneNumber,
        string? JobPosition,
        string? UserRole,
        string? Password
    )
    {
        public Domain.Entities.User ToDomain()
        {
            if(!Enum.TryParse(UserRole, out Role role))
                throw new ArgumentException(ErrorMessage.UserRoleNotFound);
            
            return new Domain.Entities.User(
                Name ?? string.Empty,
                new Email(Email ?? string.Empty),
                Password ?? string.Empty,
                role,
                JobPosition ?? string.Empty,
                SmartPhoneNumber?? string.Empty);
        }
    }
}
