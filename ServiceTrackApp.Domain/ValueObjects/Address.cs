using System.Text.RegularExpressions;
using ServiceTrackApp.Domain.Common.Erros;

namespace ServiceTrackApp.Domain.ValueObjects;

public record Address
{
    private const string PostalCodePattern = @"^(\d{5})(\d{3})$";
    
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string Country { get; init; }
    public string PostalCode { get; init; }
    
    public Address(
        string street,
        string city,
        string state,
        string country,
        string postalCode)
    {
        if(string.IsNullOrWhiteSpace(street))
            throw new ArgumentNullException(nameof(street));
        if(string.IsNullOrWhiteSpace(city))
            throw new ArgumentNullException(nameof(city));
        if(string.IsNullOrWhiteSpace(state))
            throw new ArgumentNullException(nameof(state));
        if(string.IsNullOrWhiteSpace(country))
            throw new ArgumentNullException(nameof(country));
        
        
        if(string.IsNullOrWhiteSpace(postalCode) || !Regex.IsMatch(postalCode, PostalCodePattern))
            throw new ArgumentException(ErrorMessage.InvalidPostalCode);
        
        Street = street;
        City = city;
        State = state;
        Country = country;
        PostalCode = postalCode;
    }
    
}