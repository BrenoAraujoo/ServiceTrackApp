using Microsoft.EntityFrameworkCore;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Interfaces;
using ServiceTrackHub.Domain.Pagination;
using ServiceTrackHub.Infra.Data.Context;
using ServiceTrackHub.Infra.Data.Helpers;
using System.Linq.Dynamic.Core;
using Microsoft.Data.SqlClient;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.CustomExceptions;
using ServiceTrackHub.Domain.Filters;

namespace ServiceTrackHub.Infra.Data.Repositories
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
