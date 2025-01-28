namespace ServiceTrackApp.Application.InputViewModel.Contact;

public record ContactUpdateModel(
    Guid? Id,
    string? Name,
    string? JobPosition,
    string? Email,
    string? SmartPhoneNumber
);