using System.ComponentModel.DataAnnotations;

namespace ServiceTrackHub.Application.InputViewModel.TaskType
{
public record CreateTaskTypeModel(
    
    Guid CreatorId,
    string Name,
    string? Description
    );
}
