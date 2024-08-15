using System.ComponentModel.DataAnnotations;

namespace ServiceTrackHub.Application.InputViewModel.TaskType
{
public record CreateTaskTypeInputModel(

    [Required] 
    Guid creatorId,
    [Required]
    string name,
    [Required]
    string description
    );
}
