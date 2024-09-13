
using System.ComponentModel.DataAnnotations;
using ServiceTrackHub.Domain.CustomDataAnnotations;

namespace ServiceTrackHub.Application.InputViewModel.User
{
    public record CreateUserModel(

        string Name,
        [EmailCustomDataAnnotation]
        string Email,
        [PhoneCustomDataAnnotation]
        string Phone,
        [PasswordCustomDataAnnotation]
        string Password
        );

}
