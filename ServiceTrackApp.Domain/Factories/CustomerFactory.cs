using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.ValueObjects;

namespace ServiceTrackApp.Domain.Factories;

public static class CustomerFactory
{
    public static Customer Create (
        string cpfCnpj,
        string name,
        string email,
        string smartPhoneNumber,
        string street,
        string city,
        string state,
        string postalCode,
        string country,
        IList<Contact>? contacts)
    {
    //Value objects
    var cpfCnpjValue = new CpfCnpj(cpfCnpj);
    var emailValue = new Email(email);
    var smartPhoneValue = new SmartPhoneNumber(smartPhoneNumber);
    var address = new Address(street, city, state, country, postalCode);
    var customer = new Customer(name, emailValue, smartPhoneValue, address, cpfCnpjValue);
    
    if(contacts is null)
        return customer;
    foreach (var contact in contacts)
        customer.AddContact(contact);
    return customer;
    }
}