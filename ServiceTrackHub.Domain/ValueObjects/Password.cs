using System.Text.RegularExpressions;
using ServiceTrackHub.Domain.Common.Erros;

namespace ServiceTrackHub.Domain.ValueObjects;

public class Password : ValueObject
{

    public string Value { get; set; }

    public Password(string value)
    {
        if (!string.IsNullOrEmpty(value) && !IsValid(value))
            throw new ArgumentException(ErrorMessage.InvalidPassword);
        Value = value;
    }

    public override bool IsValid(object value)
    {
        var valueString = value as string;
        if (string.IsNullOrWhiteSpace(valueString) || valueString.Length > 20 || valueString.Length < 10)
            return false;
        
        var pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
        return Regex.IsMatch(valueString, pattern);
        
    }
    
    
}