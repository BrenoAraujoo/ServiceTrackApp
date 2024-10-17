using Microsoft.EntityFrameworkCore;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Interfaces;
using ServiceTrackHub.Domain.Pagination;
using ServiceTrackHub.Infra.Data.Context;
using ServiceTrackHub.Infra.Data.Helpers;
using System.Linq.Dynamic.Core;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Filters;
using ServiceTrackHub.Domain.ValueObjects;

namespace ServiceTrackHub.Infra.Data.Repositories
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

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
