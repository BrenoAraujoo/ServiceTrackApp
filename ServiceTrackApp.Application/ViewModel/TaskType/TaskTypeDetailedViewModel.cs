namespace ServiceTrackApp.Application.ViewModel.TaskType
{
   public record TaskTypeDetailedViewModel
    {
        public Guid Id { get; set; }
        public Guid CreatorId { get;  set; }
        public string  CreatorName { get; set; }
        public string Name { get;  set; }
        public string? Description { get;  set; }
        public bool Active { get;  set; }

        public DateTime CreationDate { get;  set; }
        public DateTime UpdateDate{ get;  set; }

        public static TaskTypeDetailedViewModel ToViewModel(Domain.Entities.TaskType taskType, Domain.Entities.User user)
        {
            return new TaskTypeDetailedViewModel
            {
                Id = taskType.Id,
                CreatorId = taskType.CreatorId,
                Name = taskType.Name,
                Description = taskType.Description,
                Active = taskType.Active,
                CreationDate = taskType.CreationDate,
                UpdateDate = taskType.UpdateDate,
                CreatorName = user.Name
            };
        }

        public static List<TaskTypeDetailedViewModel> ToViewModel(List<Domain.Entities.TaskType> taskTypes, Domain.Entities.User user)
        {
            return taskTypes.Select(task => ToViewModel(task, user)).ToList();
        }
    }
}
