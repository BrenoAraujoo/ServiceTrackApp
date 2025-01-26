using ServiceTrackApp.Application.InputViewModel.Contact;
using ServiceTrackApp.Application.InputViewModel.Task;
using ServiceTrackApp.Application.Interfaces.Domain;
using ServiceTrackApp.Domain.Common.Erros;
using ServiceTrackApp.Domain.Common.Result;
using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Interfaces;
using ServiceTrackApp.Domain.Pagination;
using ServiceTrackApp.Domain.ValueObjects;

namespace ServiceTrackApp.Application.Services.Domain;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;

    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }


    public Task<Result> GetAll(IFilterCriteria<Contact> filter, PaginationRequest pagination)
    {
        throw new NotImplementedException();
    }

    public Task<Result> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result> GetByCustomerId(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Create(CreateContactModel contactModel)
    {
        try
        {
            var contactEntity = CreateContact(contactModel);
            await _contactRepository.CreateAsync(contactEntity);
            return Result.Success();
        }
        catch (ArgumentException e)
        {
            return Result.Failure(CustomError.ValidationError(e.Message));
        }
        catch (Exception e)
        {
            return Result.Failure(CustomError.ServerError(e.Message));
        }
        
    }

    public Task<Result> Update(Guid taskId, CreateContactModel contactModel)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    private static Contact CreateContact(CreateContactModel contactModel)
    {
        return new Contact
        (contactModel.Name,
            new JobPosition(contactModel.JobPosition),
            new Email(contactModel.Email ?? string.Empty),
            new SmartPhoneNumber(contactModel.SmartPhoneNumber),
            contactModel.CustomerId);
    }
}