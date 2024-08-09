using Microsoft.EntityFrameworkCore;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Interfaces;
using ServiceTrackHub.Infra.Data.Context;

namespace ServiceTrackHub.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> CreateAsync(User user)
        {
            _context.Add(user);
            var result = await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
           return await _context.Users
                .AsNoTracking()
                .ToListAsync();
            
        }

        public async Task<User?> GetByIdAsync(Guid? id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id==id);
            return user;
        }

        public async Task<User> RemoveAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

    }
}
