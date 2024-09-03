using System.ComponentModel.DataAnnotations;

namespace ServiceTrackHub.Application.InputViewModel.Task
{
    public record TasksInputModel (
       [Required(ErrorMessage = "O nome da tarefa é obrigatório"), 
        MinLength (1,ErrorMessage = "O tamanho mínimo do nome da tarefe é 1"), 
        MaxLength (50,ErrorMessage = "O tamanho máximo do nome da tarefe é 50")]
        string Name,
        [Required(ErrorMessage = "A descrição da tarefa é obrigatória"),
        MinLength (3,ErrorMessage = "O tamanho mínimo do nome da tarefe é 3"),
        MaxLength (100,ErrorMessage = "O tamanho máximo do nome da tarefe é 100")]
        string Description,
        [Required(ErrorMessage = "O id do usuário é obrigatório")]
        Guid UserId,
        Guid? UserToId,
        [Required(ErrorMessage = "O id do tipo de tarefa é obrigatório")]
        Guid TaskTypeId);
}
