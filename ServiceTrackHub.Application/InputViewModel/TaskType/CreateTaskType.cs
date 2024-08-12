using System.ComponentModel.DataAnnotations;

namespace ServiceTrackHub.Application.InputViewModel.TaskType
{
public record CreateUserInputModel(
    [Required]
    string name,
    [Required]
    string description
    );
}
