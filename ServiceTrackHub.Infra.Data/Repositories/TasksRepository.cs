using Microsoft.EntityFrameworkCore;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Interfaces;
using ServiceTrackHub.Infra.Data.Context;

namespace ServiceTrackHub.Infra.Data.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        ApplicationDbContext _context;
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

        public async Task<IEnumerable<Tasks>> GetAllAsync()
        {
            return await _context.Tasks
                .AsNoTracking()
                .ToListAsync(); 
        }

        public async Task<Tasks> GetByIdAsync(int? id)
        {
            return await _context.Tasks
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Tasks>> GetServicesByUserIdAsync(int userId)
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
