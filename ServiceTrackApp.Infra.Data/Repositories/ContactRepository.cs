using Microsoft.EntityFrameworkCore;
using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Interfaces;
using ServiceTrackApp.Domain.Pagination;
using ServiceTrackApp.Infra.Data.Context;

namespace ServiceTrackApp.Infra.Data.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly ApplicationDbContext _context;

    public ContactRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<PagedList<Contact>> GetAllAsync(IFilterCriteria<Contact> filter, PaginationRequest paginationRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<Contact?> GetByIdAsync(Guid id)
    {
        return await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Contact> CreateAsync(Contact contact)
    {
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
        return contact;
    }

    public Task<Contact> UpdateAsync(Contact trackedContact)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Contact contact)
    {
        throw new NotImplementedException();
    }
}