using System.Text.RegularExpressions;
using ServiceTrackApp.Domain.Common.Erros;

namespace ServiceTrackApp.Domain.ValueObjects;

public class SmartPhoneNumber : ValueObject
{
    public string? Value { get; private set; }

    public SmartPhoneNumber(string? value)
    {
        if (!string.IsNullOrEmpty(value) && !IsValid(value))
            throw new ArgumentException(ErrorMessage.InvalidPhone);
        Value = value;
    }

    public override bool IsValid(object value)
    { 
        if (value is not string valueString) return false;
        const string pattern = @"^\d{11}$";
        return Regex.IsMatch(valueString, pattern);
    }
    
}