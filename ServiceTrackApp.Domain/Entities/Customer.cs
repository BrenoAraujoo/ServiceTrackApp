using ServiceTrackApp.Domain.ValueObjects;

namespace ServiceTrackApp.Domain.Entities;

public class Customer : BaseEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public SmartPhoneNumber? SmartPhoneNumber { get; private set; }
    public Address Address { get; private set; }

    public Customer(Guid id, string name, Email email, SmartPhoneNumber? smartPhoneNumber, Address address)
    {
        Id = id;
        Name = name;
        Email = email;
        SmartPhoneNumber = smartPhoneNumber;
        Address = address;
    }
}