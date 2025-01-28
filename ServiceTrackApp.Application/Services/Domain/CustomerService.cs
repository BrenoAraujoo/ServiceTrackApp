using ServiceTrackApp.Application.InputViewModel.Contact;
using ServiceTrackApp.Application.InputViewModel.Customer;
using ServiceTrackApp.Application.Interfaces.Domain;
using ServiceTrackApp.Application.ViewModel.Customer;
using ServiceTrackApp.Domain.Common.Erros;
using ServiceTrackApp.Domain.Common.Result;
using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Factories;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Interfaces;
using ServiceTrackApp.Domain.Pagination;
using ServiceTrackApp.Domain.ValueObjects;

namespace ServiceTrackApp.Application.Services.Domain;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository, IContactRepository contactRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result> GetAll(IFilterCriteria<Customer> filter, PaginationRequest pagination)
    {
        try
        {
            var pagedList = await _customerRepository.GetAllAsync(filter, pagination);
            var customerSimpleViewModel = CustomerSimpleViewModel.ToViewModel(pagedList.EntityList);
            var pagedViewModel = new PagedList<CustomerSimpleViewModel>(customerSimpleViewModel, pagedList.PageNumber, pagination.PageSize, pagedList.TotalItems);
            return Result<PagedList<CustomerSimpleViewModel>>.Success(pagedViewModel);
        }
        catch (Exception e)
        {
            return Result.Failure(CustomError.ServerError(e.Message));
        }
    }

    public Task<Result> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result> GetByUserId(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Create(CustomerCreateModel model)
    {
        try
        {
            var customerEntity = ToEntity(model);
            await _customerRepository.CreateAsync(customerEntity);
            return Result<Customer>.Success(customerEntity);

        }
        catch (Exception e)
        {
            return Result.Failure(CustomError.ServerError(e.Message));
        }
        
    }

    public async Task<Result> Update(Guid customerId, CustomerUpdateModel customerUpdateModel)
    {
        var customerEntity = await _customerRepository.GetByIdAsync(customerId);
        if(customerEntity is null)
            return Result.Failure(CustomError.RecordNotFound(ErrorMessage.CustomerNotFound));
        return Result.Failure(CustomError.RecordNotFound(ErrorMessage.CustomerNotFound));
        
        /*
        try
        {
            customerEntity.Update(customerUpdateModel.Name,
                new Email(customerUpdateModel.Email),
                new SmartPhoneNumber(customerUpdateModel.SmartPhoneNumber),
                new Address(
                    customerUpdateModel.Street,
                    customerUpdateModel.City,
                    customerUpdateModel.State,
                    customerUpdateModel.Country,
                    customerUpdateModel.PostalCod),
                new CpfCnpj(customerUpdateModel.CpfCnpj),null);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        */
   
    }

    public Task<Result> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
    
    private static Customer ToEntity(CustomerCreateModel model)
    {
        List<Contact>? contacts = new ();
        if (model.Contacts is not null)
        {
            contacts = model.Contacts.Select(
                c => new Contact(
                    c.Name,
                    new JobPosition(c.JobPosition),
                    c.Email is not null ? new Email(c.Email) : null,
                    new SmartPhoneNumber(c.SmartPhoneNumber)))
                .ToList();
        }
        
        var customer  = CustomerFactory.Create(
            model.CpfCnpj,
            model.Name,
            model.Email,
            model.SmartPhoneNumber,
            model.Street,
            model.City,
            model.State,
            model.PostalCod,
            model.Country,
            contacts);

        return customer;
    }

    
    /*
    private static (string? CpfCnpj,
        string? Name,
        string? Email,
        string? SmartPhoneNumber,
        string? Street,
        string? City,
        string? State,
        string? Country,
        string? PostalCod,
        IList<ContactUpdateModel>? Contacts) CreateCustomerUpdateTuple (CustomerCreateModel customerCreateModel)
    {
        var name = !string.IsNullOrEmpty(customerCreateModel.Name) ? customerCreateModel.Name :
                null;
        var email = !string.IsNullOrWhiteSpace(customerCreateModel.Email)? new Email(customerCreateModel.Email) :
        null;
        
        var smartPhoneNumber = !string.IsNullOrEmpty(customerCreateModel.SmartPhoneNumber) ? customerCreateModel.SmartPhoneNumber :
            null;
        
        
    }
    */
}