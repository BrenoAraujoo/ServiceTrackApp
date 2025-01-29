using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Interfaces;
using ServiceTrackApp.Domain.Pagination;
using ServiceTrackApp.Infra.Data.Context;
using ServiceTrackApp.Infra.Data.Helpers;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace ServiceTrackApp.Infra.Data.Repositories;

public class CustomerRepository (ApplicationDbContext context) : ICustomerRepository
{
    public async Task<PagedList<Customer>> GetAllAsync(IFilterCriteria<Customer> filter, PaginationRequest paginationRequest)
    {
        var query = context.Customers.AsQueryable();
        
        query = query.Include(c => c.Contacts);
        query = filter.Apply(query);
        
        
        query = !string.IsNullOrEmpty(paginationRequest.OrderBy) ? query.OrderBy(paginationRequest.OrderBy) :
            query.OrderBy(x => x.Email.Value);

        
        return await PaginationHelper.ToPagedListAsync(query, paginationRequest.PageNumber, paginationRequest.PageSize);
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
       return await context.Customers.FirstOrDefaultAsync(c => c.Id == id);
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

    public async Task<Customer> UpdateAsync(Customer trackedCustomer)
    {
        context.Customers.Update(trackedCustomer);
        await context.SaveChangesAsync();
        return trackedCustomer;
    }

    public Task RemoveAsync(Customer customer)
    {
        throw new NotImplementedException();
    }
}