using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Interfaces;
using ServiceTrackApp.Domain.Pagination;
using ServiceTrackApp.Infra.Data.Context;
using ServiceTrackApp.Infra.Data.Helpers;

namespace ServiceTrackApp.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
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

        public async Task<PagedList<User>> GetAllAsync(IFilterCriteria<User> filter, PaginationRequest paginationRequest)
        {
            
            var query = _context.Users.AsQueryable();
            query = filter.Apply(query);
            
            //TODO Tratar Order By usando propriedades inexistentes, para evitar erro.
            query = !string.IsNullOrEmpty(paginationRequest.OrderBy) ? query.OrderBy(paginationRequest.OrderBy) :
                query.OrderBy(x => x.Email);

            
            return await PaginationHelper.ToPagedListAsync(query, paginationRequest.PageNumber, paginationRequest.PageSize);

        }
        
        public async Task<User?> GetByIdAsync(Guid id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id==id);
            return user;
        }
        

        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(user => user.Email==email);
            return user;
        }

        public async Task RemoveAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRefreshTokenAsync(User user, string refreshToken, DateTime? refreshTokenExpiresAt)
        {

            var entry = _context.Entry(user);
            
            if (user.RefreshTokenHash != refreshToken)
            {
                entry.Property(x => x.RefreshTokenHash).IsModified = true;
                user.UpdateRefreshToken(refreshToken);
                user.UpdateRefreshTokenExpiresAt(refreshTokenExpiresAt);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserRefreshToken(User user)
        {
            var entry = _context.Entry(user);
            if (user.RefreshTokenHash != null)
            {
                entry.Property(x => x.RefreshTokenHash).IsModified = true;
                user.UpdateRefreshToken(null);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
