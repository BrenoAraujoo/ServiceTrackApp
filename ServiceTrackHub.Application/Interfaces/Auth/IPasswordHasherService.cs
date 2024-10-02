using ServiceTrackHub.Domain.Common.Result;

namespace ServiceTrackHub.Application.Interfaces.Auth;

public interface IPasswordHasherService
{
    Result <string> HashPassword(string password);
    Result VerifyPassword(string password, string storedHash);
}