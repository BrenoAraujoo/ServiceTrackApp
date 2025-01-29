namespace ServiceTrackApp.Application.InputViewModel.Contact;

public record ContactUpdateModel(
    string? Name,
    string? JobPosition,
    string? Email,
    string? SmartPhoneNumber
);