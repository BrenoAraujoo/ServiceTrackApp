using ServiceTrackApp.Domain.ValueObjects;

namespace ServiceTrackApp.Application.InputViewModel.Customer;

public record CustomerCreateModel(
    string Name,
    string Email,
    string? SmartPhoneNumber,
    string Street,
    string City,
    string State,
    string Country,
    string PostalCod);