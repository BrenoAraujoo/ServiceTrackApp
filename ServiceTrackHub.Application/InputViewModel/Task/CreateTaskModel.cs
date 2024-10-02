namespace ServiceTrackHub.Application.InputViewModel.Task
{
    public record CreateTaskModel (

        string Description,
        Guid UserId,
        Guid? UserToId,
        Guid TaskTypeId);
}
