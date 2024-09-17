namespace ServiceTrackHub.Domain.Enums.ValueObjects;

public abstract class ValueObject
{
    public  abstract bool IsValid(object value);
    
}