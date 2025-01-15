using ServiceTrackHub.Domain.Common.Result;

namespace ServiceTrackHub.Application.Interfaces.Auth;

public interface IHashService
{
    Result <string> Hash(string input);
    Result Verify(string input, string storedHash);
}