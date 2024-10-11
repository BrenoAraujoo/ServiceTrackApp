using ServiceTrackHub.Domain.Parameters;

namespace ServiceTrackHub.Application.Parameters;

public class UserRequestParameters : RequestParameters
{
    public string Name { get; set; } = string.Empty;
}