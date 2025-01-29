using ServiceTrackApp.Application.ViewModel.Contact;

namespace ServiceTrackApp.Application.ViewModel.Customer;

public record CustomerDetailedViewModel
{
    public Guid Id { get; init; }
    public string CpfCnpj { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string? SmartPhoneNumber { get; init; }
    public string Address { get; init; }
    public IEnumerable<ContactDetailedViewModel>? Contacts { get; init; }

    public static CustomerDetailedViewModel ToViewModel(Domain.Entities.Customer customer)
    {
        return new CustomerDetailedViewModel()
        {
            Id = customer.Id,
            CpfCnpj = customer.CpfCnpj.Value,
            Name = customer.Name,
            Email = customer.Email.Value,
            SmartPhoneNumber = customer.SmartPhoneNumber.Value,
            Address = FormattedAddress(customer),
            Contacts = ContactDetailedViewModel.ToViewModel(customer.Contacts)
        };
    }
    public static IEnumerable<CustomerDetailedViewModel> ToViewModel(IEnumerable<Domain.Entities.Customer> customers)
    {
        return customers.Select(ToViewModel).ToList();
    }
    private static string FormattedAddress(Domain.Entities.Customer customer)
    {
        return $"{customer.Address.Street} - {customer.Address.City} - {customer.Address.State}" +
               $" - CEP {customer.Address.PostalCode}";
    }
};