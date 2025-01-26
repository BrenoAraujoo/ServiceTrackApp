using ServiceTrackApp.Application.InputViewModel.Contact;
using ServiceTrackApp.Application.InputViewModel.Task;
using ServiceTrackApp.Domain.Common.Result;
using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Pagination;

namespace ServiceTrackApp.Application.Interfaces.Domain;

public interface IContactService
{
    Task<Result> GetAll(IFilterCriteria<Contact> filter, PaginationRequest pagination);
    Task<Result> GetById(Guid id);
    Task<Result> GetByCustomerId(Guid userId);
    Task<Result> Create(CreateContactModel contactModel);
    Task<Result> Update(Guid taskId, CreateContactModel contactModel);
    Task<Result> Delete(Guid id);
}