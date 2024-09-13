using System.ComponentModel.DataAnnotations;
using ServiceTrackHub.Domain.ValueObjects;

namespace ServiceTrackHub.Domain.CustomDataAnnotations;

public class PhoneCustomDataAnnotation : ValidationAttribute
{
    public PhoneCustomDataAnnotation()
    {
        ErrorMessage = Common.Erros.ErrorMessage.InvalidPhone;
    }

    public override bool IsValid(object? value)
    {
        var strValue = value as string;
        return Phone.IsValidPhone(strValue);
    }
}