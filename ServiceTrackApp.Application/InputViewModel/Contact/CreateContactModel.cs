namespace ServiceTrackApp.Application.InputViewModel.Contact;

public record CreateContactModel(
    string Name,
    string? JobPosition,
    string? Email,
    string SmartPhoneNumber,
    Guid CustomerId
);