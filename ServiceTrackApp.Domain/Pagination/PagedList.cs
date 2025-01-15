namespace ServiceTrackApp.Domain.Pagination;

public class PagedList<T>
{
    public IEnumerable<T> EntityList { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    private int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;

    public PagedList(IEnumerable<T> entityList, int pageNumber, int pageSize, int totalItems)
    {
        EntityList = entityList;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItems = totalItems;
    }
}
