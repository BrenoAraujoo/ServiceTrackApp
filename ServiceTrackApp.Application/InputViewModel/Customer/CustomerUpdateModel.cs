using ServiceTrackApp.Application.InputViewModel.Contact;

namespace ServiceTrackApp.Application.InputViewModel.Customer;

public record CustomerUpdateModel(
    
    string? CpfCnpj,
    string? Name,
    string? Email,
    string? SmartPhoneNumber,
    string? Street,
    string? City,
    string? State,
    string? Country,
    string? PostalCod,
    IList<ContactUpdateModel>? Contacts);