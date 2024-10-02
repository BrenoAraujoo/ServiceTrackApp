using ServiceTrackHub.Domain.Common.Erros;

namespace ServiceTrackHub.Domain.ValueObjects;

public class JobPosition : ValueObject
{
    public string? Value { get; set; }

    public JobPosition(string? value)
    {
        if(!IsValidJobPosition(value))
            throw new ArgumentException(ErrorMessage.InvalidJobPosition);
        
        Value = value;
    }
    
    public static bool IsValidJobPosition(string? value)
    {
        if (value == null) return true;
        return value.Length is  <= 40;
    }
    
    public override bool IsValid(object value)
    {
        return IsValidJobPosition(value as string);
    }
    
}