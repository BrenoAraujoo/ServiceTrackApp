using System.Text.RegularExpressions;
using ServiceTrackApp.Domain.Common.Erros;

namespace ServiceTrackApp.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; private set; }


    public Email(string value)
    {
        if (string.IsNullOrEmpty(value)|| !IsValid(value))
            throw new ArgumentException(ErrorMessage.InvalidEmail);
        Value = value;
    }

    public override bool IsValid(object value)
    {
        var valueString = value as string;
        if (valueString.Length is > 40 or < 8)
            return false;

        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(valueString, pattern);
    }
    
}