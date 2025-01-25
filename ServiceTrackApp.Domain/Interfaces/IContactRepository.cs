using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Pagination;

namespace ServiceTrackApp.Domain.Interfaces;

public interface IContactRepository
{
    Task<PagedList<Contact>> GetAllAsync(IFilterCriteria<Contact> filter, PaginationRequest paginationRequest);
    Task<Contact?> GetByIdAsync(Guid id);
    Task<Contact> CreateAsync(Contact contact);
    Task<Contact> UpdateAsync(Contact trackedContact);
    Task RemoveAsync (Contact contact);
}