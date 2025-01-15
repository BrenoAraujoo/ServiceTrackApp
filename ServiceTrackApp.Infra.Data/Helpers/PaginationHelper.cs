using Microsoft.EntityFrameworkCore;
using ServiceTrackApp.Domain.Pagination;

namespace ServiceTrackApp.Infra.Data.Helpers;

public static class PaginationHelper
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>
        (IQueryable<T> query, int pageNumber, int pageSize) where T : class
    {
        var count = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize)
            .AsNoTracking()
            .Take(pageSize).ToListAsync();
        return new PagedList<T>(items, pageNumber, pageSize, count);
    }
}