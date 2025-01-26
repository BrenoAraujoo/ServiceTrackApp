using ServiceTrackApp.Domain.Interfaces;
using ServiceTrackApp.Domain.ValueObjects;

namespace ServiceTrackApp.Domain.Entities;

public class Customer : BaseEntity, IEntityActivable
{
    public CpfCnpj CpfCnpj { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public SmartPhoneNumber SmartPhoneNumber { get; private set; }
    public Address Address { get; private set; }
    public IReadOnlyCollection<Tasks> Tasks { get; private set; } = new List<Tasks>();
    
    public IList<Contact> Contacts { get; private set; } = new List<Contact>();
    
    protected  Customer() { } //EF default constructor
    public Customer(string name, Email email, SmartPhoneNumber smartPhoneNumber, Address address, CpfCnpj cpfCnpj)
    {
        Name = name;
        Email = email;
        SmartPhoneNumber = smartPhoneNumber;
        Address = address;
        CpfCnpj = cpfCnpj;
    }

    public void AddContact(Contact contact)
    {
        Contacts.Add(contact);
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