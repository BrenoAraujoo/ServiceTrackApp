using System.Text.RegularExpressions;
using ServiceTrackHub.Domain.Common.Erros;

namespace ServiceTrackHub.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; private set; }


    public Email(string value)
    {
        if (!IsValidEmail(value))
            throw new ArgumentException(ErrorMessage.InvalidEmail);
        Value = value;
    }

    public static bool IsValidEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 40 || value.Length < 8)
            return false;

        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(value, pattern);
    }

    public override bool IsValid(object value)
    {
        return IsValidEmail(value as string);
    }
}