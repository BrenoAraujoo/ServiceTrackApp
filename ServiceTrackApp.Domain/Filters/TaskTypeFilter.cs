using ServiceTrackApp.Domain.Entities;

namespace ServiceTrackApp.Domain.Filters;

public class TaskTypeFilter : IFilterCriteria<TaskType>
{
    public string? Name { get; set; }
    public IQueryable<TaskType> Apply(IQueryable<TaskType> query)
    {
        if(Name is not null)
            query = query.Where(t => t.Name == Name);
        return query;
    }
}