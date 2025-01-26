using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Interfaces;
using ServiceTrackApp.Domain.Pagination;
using ServiceTrackApp.Infra.Data.Context;
using ServiceTrackApp.Infra.Data.Helpers;

namespace ServiceTrackApp.Infra.Data.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly ApplicationDbContext _context;
        public TasksRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tasks> CreateAsync(Tasks tasks)
        {
            _context.Add(tasks);
            await _context.SaveChangesAsync();
            return tasks;
        }

        public async Task<PagedList<Tasks>> GetAllAsync(IFilterCriteria<Tasks> filter, PaginationRequest pagination)
        {
            var query = _context.Tasks.AsQueryable();
            query = filter.Apply(query);
            return await PaginationHelper.ToPagedListAsync(query,pagination.PageNumber, pagination.PageSize);
        }

        public async Task<List<Tasks>> GetFilteredAsync(Expression<Func<Tasks, bool>> predicate)
        {
            return await _context.Tasks.Where(predicate).ToListAsync();
        }

        public async Task<Tasks?> GetByIdAsync(Guid? id)
        {
            return await _context.Tasks
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Tasks>> GetByUserIdAsync(Guid? userId)
        { 
            //Eager loading
            return await _context.Tasks.Include(u => u.User)
                .Where(s => s.UserId == userId)
                .ToListAsync();
                    
        }

        public async Task<Tasks> RemoveAsync(Tasks tasks)
        {
            _context.Remove(tasks);
            await _context.SaveChangesAsync();
            return tasks;
        }

        public async Task<Tasks> UpdateAsync(Tasks tasks)
        {
            _context.Update(tasks);
            await _context.SaveChangesAsync();
            return tasks;
        }

    }
}
