using System.Text.RegularExpressions;
using ServiceTrackHub.Domain.Common.Erros;

namespace ServiceTrackHub.Domain.ValueObjects;

public class SmartPhoneNumber : ValueObject
{
    public string Value { get; private set; }

    public SmartPhoneNumber(string value)
    {
        if (string.IsNullOrEmpty(value)||!IsValid(value))
            throw new ArgumentException(ErrorMessage.InvalidPhone);
        Value = value;
    }

    public override bool IsValid(object value)
    {
        var valueString = value as string;
        if (valueString is  null && valueString.Length != 11)
            return false;

        string pattern = @"^\d{11}$";
        return Regex.IsMatch(valueString, pattern);
    }
    
}