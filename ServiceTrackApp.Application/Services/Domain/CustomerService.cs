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
       
        var (cpfCnpj, email, smartPhoneNumber,address, contacts) = 
            CreateCustomerValueObjects(customerUpdateModel, customerEntity);

        try
        {
            customerEntity.Update(
                customerUpdateModel.Name,
                email,
                smartPhoneNumber,
                address,
                cpfCnpj,
                contacts);
            await _customerRepository.UpdateAsync(customerEntity);
            var customerSimpleViewModel = CustomerSimpleViewModel.ToViewModel(customerEntity);
            return Result<CustomerSimpleViewModel>.Success(customerSimpleViewModel);

        }
        catch (ArgumentException e)
        {
            return Result.Failure(CustomError.ValidationError("erro ao atualizar Customer"));
        }
        catch (Exception e)
        {
                return Result.Failure(CustomError.ServerError("erro ao atualizar Customer"));
        }
        
   
    }

    public Task<Result> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
    
    private static Customer ToEntity(CustomerCreateModel model)
    {
        List<Contact>? contacts = null;
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

    private static (CpfCnpj? cpfCnpj, Email? email, SmartPhoneNumber? smartPhobeNumber,Address address, List<Contact>? contacts) 
        CreateCustomerValueObjects(CustomerUpdateModel model, Customer entity)
    {
        var cpfCnpj = string.IsNullOrWhiteSpace(model.CpfCnpj) ? null : new CpfCnpj(model.CpfCnpj);
        var email = string.IsNullOrWhiteSpace(model.Email) ? null : new Email(model.Email);
        var smartPhoneNumber = string.IsNullOrWhiteSpace(model.SmartPhoneNumber) ? null : new SmartPhoneNumber(model.SmartPhoneNumber);
        
        var address = new Address(
            model.Street ?? entity.Address.Street,
            model.City ?? entity.Address.City,
            model.State ?? entity.Address.State,
            model.Country ?? entity.Address.Country,
            model.PostalCod ?? entity.Address.PostalCode
        );

        List<Contact>? contacts = null;
        if (model.Contacts is not null)
        {
            contacts = model.Contacts.Select(
                    c => new Contact(
                        c.Name ?? entity.Name,
                        new JobPosition(c.JobPosition),
                        c.Email is not null ? new Email(c.Email) : null,
                        new SmartPhoneNumber(c.SmartPhoneNumber)))
                .ToList();
        }

        return (cpfCnpj, email, smartPhoneNumber, address, contacts);
    }
    
}