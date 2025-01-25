using System.Text.RegularExpressions;
using ServiceTrackApp.Domain.Common.Erros;

namespace ServiceTrackApp.Domain.ValueObjects;

public record Password
{
    private const string Pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
    public string Value { get; private set; }

    /// <summary>
    /// Default constructor for EF. Required in this record because of 'isHashed' parameter.
    /// </summary>
    protected Password() { }

    /// <summary>
    /// Creates a Password Value object. If the 'isHashed' parameter is 'true', regular expression validation is ignored.
    /// Use the 'isHashed' parameter as 'true' when you need to bypass regular expression checking.
    /// </summary>
    /// <param name="value">Password value</param>
    /// <param name="isHashed">Defines whether the password is in hash format</param>
    /// <exception cref="ArgumentException">Thrown when the password value is invalid.</exception>
    public Password(string value, bool isHashed = false)
    {

        if (isHashed)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException(ErrorMessage.InvalidPassword);
        }
        else
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, Pattern))
                throw new ArgumentException(ErrorMessage.InvalidPassword);
        }

        Value = value;
    }
}