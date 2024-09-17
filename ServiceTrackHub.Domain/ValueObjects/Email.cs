using System.Text.RegularExpressions;
using ServiceTrackHub.Domain.Enums.Common.Erros;

namespace ServiceTrackHub.Domain.Enums.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; private set; }


    public Email(string value)
    {
        if (string.IsNullOrEmpty(value)|| !IsValidEmail(value))
            throw new ArgumentException(ErrorMessage.InvalidEmail);
        Value = value;
    }

    public static bool IsValidEmail(string value)
    {
        if (value.Length is > 40 or < 8)
            return false;

        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(value, pattern);
    }

    public override bool IsValid(object value)
    {
        return IsValidEmail(value as string);
    }
}