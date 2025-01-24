using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Pagination;

namespace ServiceTrackApp.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<PagedList<Customer>> GetAllAsync(IFilterCriteria<Customer> filter, PaginationRequest paginationRequest);
    Task<Customer?> GetByIdAsync(Guid id);
    Task<Customer> CreateAsync(Customer customer);
    Task<Customer> UpdateAsync(Customer trackedCustomer);
    Task RemoveAsync (Customer customer);
}