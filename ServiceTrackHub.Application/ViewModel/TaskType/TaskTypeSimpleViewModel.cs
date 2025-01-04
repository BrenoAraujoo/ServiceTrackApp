namespace ServiceTrackHub.Application.ViewModel.TaskType;

public record TaskTypeSimpleViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public static TaskTypeSimpleViewModel ToViewModel(Domain.Entities.TaskType taskType)
    {
        return new TaskTypeSimpleViewModel
        {
            Id = taskType.Id, Name = taskType.Name
        };
    }

    public static List<TaskTypeSimpleViewModel> ToViewModel(IEnumerable<Domain.Entities.TaskType> taskTypes)
    {
        return taskTypes.Select(ToViewModel).ToList();
    }
}