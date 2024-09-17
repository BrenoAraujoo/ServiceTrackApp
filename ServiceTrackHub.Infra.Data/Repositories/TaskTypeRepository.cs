using Microsoft.EntityFrameworkCore;
using ServiceTrackHub.Domain.Enums.Entities;
using ServiceTrackHub.Domain.Enums.Interfaces;
using ServiceTrackHub.Infra.Data.Context;
using ServiceTrackHub.Infra.Data.Migrations;

namespace ServiceTrackHub.Infra.Data.Repositories
{
    public class TaskTypeRepository : ITaskTypeRepository
    {
        ApplicationDbContext _context;
        public TaskTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TaskType> CreateAsync(TaskType taskType)
        {
            _context.Add(taskType);
            var result = await _context.SaveChangesAsync();
            return taskType;
        }

        public async Task<List<TaskType>> GetAllAsync()
        {
            return await _context.TaskType
                .AsNoTracking()
                .ToListAsync();
            
        }

        public async Task<TaskType?> GetByIdAsync(Guid? id)
        {
            var result = await _context.TaskType
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<TaskType?> GetByNameAsync(string name)
        {
            var result = await _context.TaskType
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == name);
            return result;
        }

        public async Task RemoveAsync(TaskType taskType)
        {
            _context.Remove(taskType);
            await _context.SaveChangesAsync();
        }

        public async Task<TaskType> UpdateAsync(TaskType taskType)
        {
           _context.TaskType.Update(taskType);
            await _context.SaveChangesAsync();
            return taskType;
        }
    }
}
