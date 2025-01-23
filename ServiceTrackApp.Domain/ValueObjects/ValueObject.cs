namespace ServiceTrackApp.Domain.ValueObjects;

public abstract record ValueObject
{
    public  abstract bool IsValid(object value);
    
}