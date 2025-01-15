namespace ServiceTrackHub.Domain.CustomExceptions;

public class CustomConflictException : Exception
{
    public CustomConflictException() { }
    public CustomConflictException(string? message) : base(message) {}
}