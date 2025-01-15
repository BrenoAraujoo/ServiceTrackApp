namespace ServiceTrackApp.Application.ViewModel.TaskType;

public record TaskTypeSimpleViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }

    public static TaskTypeSimpleViewModel ToViewModel(Domain.Entities.TaskType taskType)
    {
        return new TaskTypeSimpleViewModel
        {
            Id = taskType.Id, 
            Name = taskType.Name, 
            Active = taskType.Active
        };
    }

    public static List<TaskTypeSimpleViewModel> ToViewModel(IEnumerable<Domain.Entities.TaskType> taskTypes)
    {
        return taskTypes.Select(ToViewModel).ToList();
    }
}