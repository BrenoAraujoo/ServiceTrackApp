using System.ComponentModel.DataAnnotations;
using ServiceTrackHub.Domain.Enums.ValueObjects;

namespace ServiceTrackHub.Domain.Enums.CustomDataAnnotations;

public class EmailCustomDataAnnotation : ValidationAttribute
{
    public EmailCustomDataAnnotation()
    {
        ErrorMessage = Common.Erros.ErrorMessage.InvalidEmail;
    }
    public override bool IsValid(object? value)
    {
        var strValue = value as string;
        return Email.IsValidEmail(strValue);
    }
}