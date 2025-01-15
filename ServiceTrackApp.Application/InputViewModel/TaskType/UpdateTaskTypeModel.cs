namespace ServiceTrackApp.Application.InputViewModel.TaskType;

public record UpdateTaskTypeModel(
    string? Name,
    string? Description,
    bool Active
);