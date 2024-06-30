using Microsoft.EntityFrameworkCore;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Interfaces;
using ServiceTrackHub.Infra.Data.Context;

namespace ServiceTrackHub.Infra.Data.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        ApplicationDbContext _context;
        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Service> CreateAsync(Service service)
        {
            _context.Add(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services
                .AsNoTracking()
                .ToListAsync(); 
        }

        public async Task<Service> GetByIdAsync(int? id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task<IEnumerable<Service>> GetServicesByUserIdAsync(int userId)
        {   
                                           //Eager loading
            return await _context.Services.Include(u => u.User)
                .Where(s => s.UserId == userId)
                .ToListAsync();
                    
        }

        public async Task<Service> RemoveAsync(Service service)
        {
            _context.Remove(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<Service> UpdateAsync(Service service)
        {
            _context.Update(service);
            await _context.SaveChangesAsync();
            return service;
        }
    }
}
