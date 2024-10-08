namespace ServiceTrackHub.Application.InputViewModel.Task
{
    public record UpdateTaskModel(
        string? Description,
        Guid? UserToId);
}