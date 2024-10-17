using ServiceTrackHub.Application.Interfaces.Auth;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Common.Erros;

namespace ServiceTrackHub.Application.Services.Auth;

public class HashService : IHashService
{
    public Result<string> Hash(string input)
    {
        try
        {
            return Result<string>.Success(BCrypt.Net.BCrypt.HashPassword(input));
        }
        catch (Exception e)
        {
            return Result<string>.Failure(CustomError.ValidationError(e.Message));
        }
    }

    public Result Verify(string input, string storedHash)
    {
        try
        {
            var verify = BCrypt.Net.BCrypt.Verify(input, storedHash);
            return verify ? Result.Success() : Result.Failure(CustomError.ValidationError("Invalid hash"));
        }
        catch (Exception ex)
        {
            return Result.Failure(CustomError.ValidationError(ex.Message));
        }
    }
}