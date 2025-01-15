using ServiceTrackHub.Domain.Pagination;

namespace ServiceTrackHub.Application.Parameters;

public class UserPaginationRequest 
{
    public string Name { get; set; } = string.Empty;
}