using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using ServiceTrackApp.Domain.Common.Erros;
using ServiceTrackApp.Domain.CustomExceptions;
using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Interfaces;
using ServiceTrackApp.Domain.Pagination;
using ServiceTrackApp.Infra.Data.Context;
using ServiceTrackApp.Infra.Data.Helpers;

namespace ServiceTrackApp.Infra.Data.Repositories
{
    public class TaskTypeRepository : ITaskTypeRepository
    {
        private readonly ApplicationDbContext _context;
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

        public async Task<PagedList<TaskType>> GetAllAsync(IFilterCriteria<TaskType> filter,PaginationRequest paginationRequest)
        {
            var query =  _context.TaskType.AsQueryable().AsNoTracking();
            query = filter.Apply(query);
            
            query = !string.IsNullOrEmpty(paginationRequest.OrderBy) ? query.OrderBy(paginationRequest.OrderBy) :
                query.OrderByDescending(x => x.CreationDate);
            return await PaginationHelper.ToPagedListAsync(query, paginationRequest.PageNumber, paginationRequest.PageSize);
            
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

            try
            {
                _context.Remove(taskType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new CustomConflictException(ErrorMessage.TaskTypeCantBeRemoved);
            }
        }

        public async Task<TaskType> UpdateAsync(TaskType taskType)
        {
           _context.TaskType.Update(taskType);
           await _context.SaveChangesAsync();
            return taskType;
        }
    }
}
