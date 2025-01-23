using ServiceTrackApp.Domain.Enums;

namespace ServiceTrackApp.Application.InputViewModel.Task
{
    public record CreateTaskModel (

        string Description,
        Guid UserId,
        Guid? UserToId,
        Guid TaskTypeId,
        Priority Priority,
        Status Status,
        Guid CustomerId);
}
