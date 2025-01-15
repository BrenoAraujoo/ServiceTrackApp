using System.ComponentModel.DataAnnotations;
using ServiceTrackHub.Domain.Enums.ValueObjects;

namespace ServiceTrackHub.Domain.Enums.CustomDataAnnotations;

public class SmartPhoneCustomDataAnnotation : ValidationAttribute
{
    public SmartPhoneCustomDataAnnotation()
    {
        ErrorMessage = Common.Erros.ErrorMessage.InvalidPhone;
    }

    public override bool IsValid(object? value)
    {
        var strValue = value as string;
        return SmartPhoneNumber.IsValidPhone(strValue);
    }
}