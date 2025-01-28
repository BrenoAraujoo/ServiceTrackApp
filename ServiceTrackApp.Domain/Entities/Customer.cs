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

    public void Update(
        string? name,
        string? email,
        string? smartPhoneNumber,
        string? address,
        string? cpfCnpj,
        IList<Contact>? contacts)
    {
        
        /*
        if(name is not null) Name = name;
        if(email is not null) Email = new Email(email);
        if(smartPhoneNumber is not null) SmartPhoneNumber = new SmartPhoneNumber(smartPhoneNumber);
        if(address is not null) Address = new Address(address);
        if(cpfCnpj is not null) CpfCnpj = cpfCnpj;
        */
    }
    public void Activate()
    {
        throw new NotImplementedException();
    }

    public void Deactivate()
    {
        throw new NotImplementedException();
    }

    private void UpdateCustomerContacts(List<Contact>? contacts)
    {
        if(contacts is null) return;
        
        contacts.ForEach(contact =>
        {
            var currentContact = Contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (currentContact is not null)
            {
                currentContact.Update(contact.Name, contact.JobPosition,contact.Email, contact.SmartPhoneNumber);
            }
            else
            {
                Contacts.Add(contact);
            }
        });
        
    }
    
}