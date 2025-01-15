namespace ServiceTrackHub.Domain.ValueObjects;

public abstract class ValueObject
{
    public  abstract bool IsValid(object value);
    
}