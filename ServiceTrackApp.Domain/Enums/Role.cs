using ServiceTrackApp.Domain.Common.Erros;

namespace ServiceTrackApp.Domain.Enums;

public enum Role : byte
{
    User = 0,
    Manager = 1,
    Admin = 2
}

public static class RoleExtensions
{
    public static Role? ParseOrNull(string? value)
    {
        if(!string.IsNullOrWhiteSpace(value) &&
           Enum.TryParse(value, out Role parsedRole) &&
           Enum.IsDefined(typeof(Role), parsedRole))
        {
            return parsedRole;
        }
        return null;
    }
    
    public static Role ParseRole(string role)
    {
        if(!Enum.TryParse(role, out Role parsedRole))
            throw new ArgumentException(ErrorMessage.UserRoleNotFound);
        return parsedRole;
    }
}