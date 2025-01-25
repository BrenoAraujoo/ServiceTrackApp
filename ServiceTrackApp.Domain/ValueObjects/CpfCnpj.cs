using System.Text.RegularExpressions;

namespace ServiceTrackApp.Domain.ValueObjects;

public record CpfCnpj
{
    private const string Pattern = @"^\d{11}(\d{3})?$";
    private const int CpfLength = 11;
    private const int CnpjLength = 14;
    
    public string Value { get; private set; }

    public CpfCnpj(string value)
    {
        if((value.Length != CpfLength && value.Length != CnpjLength) || !Regex.IsMatch(value, Pattern))
            throw new ArgumentException("Invalid CPF/CNPJ format.");
        
        Value = value;
    }
}