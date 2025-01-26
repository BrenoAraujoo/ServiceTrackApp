using ServiceTrackApp.Domain.Entities;

namespace ServiceTrackApp.Domain.Filters;

public class CustomerFilter : IFilterCriteria<Customer>
{

    public string? Name { get; set; }
    public IQueryable<Customer> Apply(IQueryable<Customer> query)
    {
        if(Name is not null)
            query = query.Where(u => u.Name.Contains(Name));
        return query;
    }
}