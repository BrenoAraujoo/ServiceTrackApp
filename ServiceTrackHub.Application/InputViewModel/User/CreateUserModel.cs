using System.ComponentModel.DataAnnotations;

namespace ServiceTrackHub.Application.InputViewModel.User
{
    public record CreateUserModel(
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(3, ErrorMessage ="O tamanho mínimo é 3"),
        MaxLength(100, ErrorMessage = "O tamanho máximo é 100")]
        string Name,
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage ="O e-mail não está no padrão correto")]
        string Email,
        [Required(ErrorMessage = "O Telefone é obrigatório")]
        string Phone,
        [Required(ErrorMessage = "A senha é obrigatória")]
        string Password
        );

}
