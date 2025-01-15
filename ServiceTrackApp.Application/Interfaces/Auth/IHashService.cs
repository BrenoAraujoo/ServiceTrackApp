using ServiceTrackApp.Domain.Common.Result;

namespace ServiceTrackApp.Application.Interfaces.Auth;

public interface IHashService
{
    Result <string> Hash(string input);
    Result Verify(string input, string storedHash);
}