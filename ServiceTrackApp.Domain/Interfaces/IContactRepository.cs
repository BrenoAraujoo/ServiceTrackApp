using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Pagination;

namespace ServiceTrackApp.Domain.Interfaces;

public class IContactsRepository
{
    Task<PagedList<Contact>> GetAllAsync(IFilterCriteria<Contact> filter, PaginationRequest paginationRequest);
    Task<Contact?> GetByIdAsync(Guid id);
    Task<Contact> CreateAsync(Contact contact);
    Task<Contact> UpdateAsync(Contact trackedCustomer);
    Task RemoveAsync (Contact customer);
}