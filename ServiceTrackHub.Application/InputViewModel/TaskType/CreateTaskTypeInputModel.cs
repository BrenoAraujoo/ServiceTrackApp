using System.ComponentModel.DataAnnotations;

namespace ServiceTrackHub.Application.InputViewModel.TaskType
{
public record CreateTaskTypeInputModel(

    [Required(ErrorMessage ="O id do usuário que criou a tarefa é obrigatório")] 
    Guid creatorId,
    [Required(ErrorMessage ="O nome da tarefa é obrigatório")]
    string name,
    string? description
    );
}
