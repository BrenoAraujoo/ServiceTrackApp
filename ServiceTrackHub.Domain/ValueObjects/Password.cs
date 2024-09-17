using System.Text.RegularExpressions;
using ServiceTrackHub.Domain.Enums.Common.Erros;

namespace ServiceTrackHub.Domain.Enums.ValueObjects;

public class Password : ValueObject
{

    public string Value { get; set; }

    public Password(string value)
    {
        if (!IsValidPassword(value))
            throw new ArgumentException(ErrorMessage.InvalidPassword);
        Value = value;
    }

    public static bool IsValidPassword(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 20 || value.Length < 10)
            return false;
        
        string pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
        return Regex.IsMatch(value, pattern);
        
    }
    
    public override bool IsValid(object value)
    {
        return IsValidPassword(value as string);
    }
    
    
}