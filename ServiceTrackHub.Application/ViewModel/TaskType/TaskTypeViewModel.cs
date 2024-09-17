namespace ServiceTrackHub.Application.ViewModel.TaskType
{
   public record TaskTypeViewModel
    {
        public Guid Id { get; set; }
        public Guid CreatorId { get;  set; }
        public string Name { get;  set; }
        public string? Description { get;  set; }
        public bool Active { get;  set; }

        public static TaskTypeViewModel ToViewModel(Domain.Entities.TaskType taskType)
        {
            return new TaskTypeViewModel
            {
                Id = taskType.Id,
                CreatorId = taskType.CreatorId,
                Name = taskType.Name,
                Description = taskType.Description,
                Active = taskType.Active
            };
        }
    }
}
