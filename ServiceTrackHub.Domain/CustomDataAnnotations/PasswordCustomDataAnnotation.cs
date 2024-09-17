using System.ComponentModel.DataAnnotations;
using ServiceTrackHub.Domain.Enums.ValueObjects;

namespace ServiceTrackHub.Domain.Enums.CustomDataAnnotations;

public class PasswordCustomDataAnnotation : ValidationAttribute
{
    public PasswordCustomDataAnnotation()
    {
        ErrorMessage = Common.Erros.ErrorMessage.InvalidPassword;
    }
    public override bool IsValid(object? value)
    {
        return Password.IsValidPassword(value as string);
    }
}