using ServiceTrackApp.Application.InputViewModel.Customer;
using ServiceTrackApp.Application.InputViewModel.Task;
using ServiceTrackApp.Domain.Common.Result;
using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Pagination;

namespace ServiceTrackApp.Application.Interfaces.Domain;

public interface ICustomerService
{
    Task<Result> GetAll(IFilterCriteria<Customer> filter, PaginationRequest pagination);
    Task<Result> GetById(Guid id);
    Task<Result> GetByUserId(Guid userId);
    Task<Result> Create(CustomerCreateModel customerCreateModel);
    Task<Result> Update(Guid taskId, UpdateTaskModel taskModel);
    Task <Result>Delete(Guid id);
}