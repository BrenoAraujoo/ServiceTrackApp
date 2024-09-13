using System.Text.RegularExpressions;
using ServiceTrackHub.Domain.Common.Erros;

namespace ServiceTrackHub.Domain.ValueObjects;

public class Phone : ValueObject
{
    public string Value { get; private set; }

    public Phone(string value)
    {
        if (!IsValidPhone(value))
            throw new ArgumentException(ErrorMessage.InvalidPhone);
        Value = value;
    }

    public static bool IsValidPhone(string value)
    {
        if (value.Length != 11)
            return false;

        string pattern = @"^\d{11}$";
        return Regex.IsMatch(value, pattern);
    }

    public override bool IsValid(object value)
    {
        return IsValidPhone(value as string);
    }
}