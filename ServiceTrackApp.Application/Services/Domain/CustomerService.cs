using ServiceTrackApp.Application.InputViewModel.Contact;
using ServiceTrackApp.Application.InputViewModel.Customer;
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

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IContactRepository _contactRepository;

    public CustomerService(ICustomerRepository customerRepository, IContactRepository contactRepository)
    {
        _customerRepository = customerRepository;
        _contactRepository = contactRepository;
    }

    public Task<Result> GetAll(IFilterCriteria<Customer> filter, PaginationRequest pagination)
    {
        throw new NotImplementedException();
    }

    public Task<Result> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result> GetByUserId(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Create(CustomerCreateModel customerCreateModel)
    {
        try
        {
            var customerEntity = CreateCustomer(customerCreateModel);
            await _customerRepository.CreateAsync(customerEntity);
            return Result<Customer>.Success(customerEntity);

        }
        catch (Exception e)
        {
            return Result.Failure(CustomError.ServerError(e.Message));
        }
        
    }

    public Task<Result> Update(Guid taskId, UpdateTaskModel taskModel)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
    
    private static Customer CreateCustomer(CustomerCreateModel customerCreateModel)
    {
         var customer = new Customer(customerCreateModel.Name,
            new Email(customerCreateModel.Email),
            new SmartPhoneNumber(customerCreateModel.SmartPhoneNumber),
            new Address
            (customerCreateModel.Street,
                customerCreateModel.City,
                customerCreateModel.State,
                customerCreateModel.Country,
                customerCreateModel.PostalCod),
            new CpfCnpj(customerCreateModel.CpfCnpj));

         foreach (var contact in customerCreateModel.Contacts)
             customer.AddContact(contact);
         
         return customer;
    }
}