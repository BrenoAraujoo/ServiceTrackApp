using ServiceTrackApp.Domain.Interfaces;
using ServiceTrackApp.Domain.ValueObjects;

namespace ServiceTrackApp.Domain.Entities;

public class Customer : BaseEntity, IEntityActivable
{
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public SmartPhoneNumber? SmartPhoneNumber { get; private set; }
    public Address Address { get; private set; }

    public IReadOnlyCollection<Tasks> Tasks { get; private set; } = new List<Tasks>();
    protected  Customer() { }
    public Customer(string name, Email email, SmartPhoneNumber? smartPhoneNumber, Address address)
    {
        Name = name;
        Email = email;
        SmartPhoneNumber = smartPhoneNumber;
        Address = address;
    }

    public void Activate()
    {
        throw new NotImplementedException();
    }

    public void Deactivate()
    {
        throw new NotImplementedException();
    }
}