using System.ComponentModel.DataAnnotations;

namespace ServiceTrackHub.Application.InputViewModel.User
{
    public record CreateUserInputModel(
        [Required(ErrorMessage = "The name is required")]
        [MinLength(3, ErrorMessage ="Min length must be 3"),
        MaxLength(100, ErrorMessage = "Max length must be 100")]
        string Name,
        [Required(ErrorMessage = "The email is required")]
        [EmailAddress]
        string Email,
        string Phone,
        string Password
        );

}
