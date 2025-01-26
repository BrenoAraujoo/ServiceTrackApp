using ServiceTrackApp.Domain.Entities;

namespace ServiceTrackApp.Application.InputViewModel.Customer;

public record CustomerCreateModel(
    string CpfCnpj,
    string Name,
    string Email,
    string? SmartPhoneNumber,
    string Street,
    string City,
    string State,
    string Country,
    string PostalCod,
    List<Domain.Entities.Contact>? Contacts);