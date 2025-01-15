using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Domain.Filters;

public class TasksFilter : IFilterCriteria<Tasks>
{
    public Guid? TaskTypeId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? UserToId { get; set; }

    public string? Description { get; set; }

    public IQueryable<Tasks> Apply(IQueryable<Tasks> query)
    {
        if (UserToId is not null)
            query = query.Where(t => t.UserToId == UserToId);
        if (UserId is not null)
            query = query.Where(t => t.UserId == UserId);
        if (TaskTypeId is not null)
            query = query.Where(t => t.TaskTypeId == TaskTypeId);
        if (!string.IsNullOrEmpty(Description))
            query = query.Where(t => t.Description != null && t.Description.Contains(Description));

        return query;
    }
}