using ServiceTrackApp.Domain.Entities;

namespace ServiceTrackApp.Domain.Filters;

public class UserFilter : IFilterCriteria<User>
{

    public string? Name { get; set; }
    
    public IQueryable<User> Apply(IQueryable<User> query)
    {
       
        if(Name is not null)
            query = query.Where(u => u.Name.Contains(Name));
        return query;
    }
}