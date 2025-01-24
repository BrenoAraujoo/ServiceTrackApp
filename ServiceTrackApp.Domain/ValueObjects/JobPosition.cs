using ServiceTrackApp.Domain.Common.Erros;

namespace ServiceTrackApp.Domain.ValueObjects;

public record JobPosition
{
    private const int MaxLength = 40;
    public string? Value { get; private set; }

    public JobPosition(string? value)
    {
        if(!string.IsNullOrWhiteSpace(value) && value.Length > MaxLength)
            throw new ArgumentException(ErrorMessage.InvalidJobPosition);
        Value = value;
    }
}