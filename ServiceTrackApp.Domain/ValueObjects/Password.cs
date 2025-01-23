using System.Text.RegularExpressions;
using ServiceTrackApp.Domain.Common.Erros;

namespace ServiceTrackApp.Domain.ValueObjects;

public record Password : ValueObject
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
        if (string.IsNullOrWhiteSpace(valueString) || valueString.Length > 100 || valueString.Length < 10)
            return false;
        
        var pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
        return Regex.IsMatch(valueString, pattern);
        
    }
    
    
}