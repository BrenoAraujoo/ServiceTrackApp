using System.Text.RegularExpressions;
using ServiceTrackApp.Domain.Common.Erros;

namespace ServiceTrackApp.Domain.ValueObjects;

public record Email
{
    private const string Pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    private const int MinLength = 5;
    private const int MaxLength = 50;
    public string Value { get; private set; }
    
    public Email(string value)
    {
        if (string.IsNullOrEmpty(value)|| !Regex.IsMatch(value, Pattern))
            throw new ArgumentException(ErrorMessage.InvalidEmail);
        if(value.Length is > MaxLength or < MinLength)
            throw new ArgumentException(ErrorMessage.InvalidEmailLeght);
        Value = value;
    }
}