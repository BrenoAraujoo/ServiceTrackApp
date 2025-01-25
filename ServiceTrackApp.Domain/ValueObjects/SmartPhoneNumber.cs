using System.Text.RegularExpressions;
using ServiceTrackApp.Domain.Common.Erros;

namespace ServiceTrackApp.Domain.ValueObjects;

public record SmartPhoneNumber
{
    private const string Pattern = @"^\d{11}$";
    public string? Value { get; private set; }

    public SmartPhoneNumber(string? value)
    {
        if (!string.IsNullOrEmpty(value) && !Regex.IsMatch(value, Pattern))
            throw new ArgumentException(ErrorMessage.InvalidPhone);
        Value = value;
    }
}