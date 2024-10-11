namespace ServiceTrackHub.Domain.Pagination;

public class PagedList<T>
{
    public IEnumerable<T> Result { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    private int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;

    public PagedList(IEnumerable<T> result, int pageNumber, int pageSize, int totalItems)
    {
        Result = result;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItems = totalItems;
    }
}
