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
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return customer;

        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Creating customer failed: {e.Message}");
        }
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