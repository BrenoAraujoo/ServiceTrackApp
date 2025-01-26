using System.Text.Json.Serialization;
using ServiceTrackApp.Domain.ValueObjects;

namespace ServiceTrackApp.Domain.Entities;

public class Contact : BaseEntity
{
    public string Name { get; private set; }
    public JobPosition? JobPosition { get; private set; }
    public Email? Email { get; private set; }
    public SmartPhoneNumber SmartPhoneNumber { get; private set; }
    public Guid CustomerId  { get; private set; }
    
    [JsonIgnore]
    public Customer Customer { get; private set; } // Ef nav prop

    protected Contact() { } // Ef default constructor

    /*
    public Contact(string name, JobPosition? jobPosition, Email? email, SmartPhoneNumber smartPhoneNumber, Guid customerId)
    {
        Name = name;
        JobPosition = jobPosition;
        Email = email;
        SmartPhoneNumber = smartPhoneNumber;
        CustomerId = customerId;
    }
    */
    
    public Contact(string name, JobPosition? jobPosition, Email? email, SmartPhoneNumber smartPhoneNumber)
    {
        Name = name;
        JobPosition = jobPosition;
        Email = email;
        SmartPhoneNumber = smartPhoneNumber;
    }
}