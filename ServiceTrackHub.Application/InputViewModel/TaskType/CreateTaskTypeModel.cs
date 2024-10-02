using System.ComponentModel.DataAnnotations;

namespace ServiceTrackHub.Application.InputViewModel.TaskType
{
public record CreateTaskTypeModel(
    
    Guid creatorId,
    string name,
    string? description
    );
}
