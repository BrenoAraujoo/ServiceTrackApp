namespace ServiceTrackHub.Domain.Parameters;

public abstract class RequestParameters
{
    private const int MaxPageSize = 5;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 5;
    public string? SearchTerm { get; set; }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}