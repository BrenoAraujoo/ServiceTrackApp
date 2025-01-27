namespace ServiceTrackApp.Application.ViewModel.Customer;

public record CustomerSimpleViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Address { get; init; }

    public static CustomerSimpleViewModel ToViewModel(Domain.Entities.Customer customer)
    {
        return new CustomerSimpleViewModel
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email.Value,
            Address = FormattedAddress(customer)
        };
    }

    public static IEnumerable<CustomerSimpleViewModel> ToViewModel(IEnumerable<Domain.Entities.Customer> customers)
    {
        return customers.Select(CustomerSimpleViewModel.ToViewModel).ToList();
    }

    private static string FormattedAddress(Domain.Entities.Customer customer)
    {
        return $"{customer.Address.Street} - {customer.Address.City} - {customer.Address.State}" +
               $" - CEP {customer.Address.PostalCode}";
    }
}