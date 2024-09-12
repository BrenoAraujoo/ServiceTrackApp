namespace ServiceTrackHub.Domain.ValueObjects;

public class Email
{
    public string Value { get; private set; }


    public Email(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("O email está em um formato inválido");
        if (value.Length > 40)
            throw new ArgumentException("O email deve ter no máximo 40 caracteres");
        Value = value;
    }
}