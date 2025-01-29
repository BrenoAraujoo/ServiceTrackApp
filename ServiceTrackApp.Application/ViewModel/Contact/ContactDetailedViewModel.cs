namespace ServiceTrackApp.Application.ViewModel.Contact;

public record ContactDetailedViewModel
{
    public Guid Id { get; set; }
    public string Name { get; init;}
    public string? JobPosition { get; init;}
    public string? Email { get; init; }
    public string SmartPhoneNumber { get; init; }

    public static ContactDetailedViewModel ToViewModel(Domain.Entities.Contact contact)
    {
        return new ContactDetailedViewModel
        {
            Id = contact.Id,
            Name = contact.Name,
            JobPosition = contact.JobPosition.Value,
            Email = contact.Email.Value,
            SmartPhoneNumber = contact.SmartPhoneNumber.Value
        };
    }

    public static IEnumerable<ContactDetailedViewModel> ToViewModel(IEnumerable<Domain.Entities.Contact> contacts)
    {
        return contacts.Select(ToViewModel);
    }
}