using ServiceTrackHub.Application.Interfaces.Auth;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Common.Erros;

namespace ServiceTrackHub.Application.Services.Auth;

public class PasswordHasherService : IPasswordHasherService
{
    public Result<string> HashPassword(string password)
    {
        try
        {
            return Result<string>.Success(BCrypt.Net.BCrypt.HashPassword(password));
        }
        catch
        {
            return Result<string>.Failure(CustomError.None);
        }
    }

    public Result VerifyPassword(string password, string storedHash)
    {
        try
        {
            var verify = BCrypt.Net.BCrypt.Verify(password, storedHash);
            return verify ? Result.Success() : Result.Failure(CustomError.None);
        }
        catch 
        {
            return Result.Failure(CustomError.None);
        }
    }
}