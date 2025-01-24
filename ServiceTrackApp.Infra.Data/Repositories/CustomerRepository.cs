using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Interfaces;
using ServiceTrackApp.Domain.Pagination;
using ServiceTrackApp.Infra.Data.Context;

namespace ServiceTrackApp.Infra.Data.Repositories;

public class CustomerRepository (ApplicationDbContext context) : ICustomerRepository
{
    public Task<PagedList<Customer>> GetAllAsync(IFilterCriteria<Customer> filter, PaginationRequest paginationRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        context.Add(customer);
        await context.SaveChangesAsync();
        return customer;
    }

    public Task<Customer> UpdateAsync(Customer trackedCustomer)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Customer customer)
    {
        throw new NotImplementedException();
    }
}