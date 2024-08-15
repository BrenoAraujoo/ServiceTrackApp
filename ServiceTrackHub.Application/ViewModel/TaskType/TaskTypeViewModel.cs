namespace ServiceTrackHub.Application.ViewModel.TaskType
{
   public record TaskTypeViewModel
    {
        public Guid Id { get; set; }
        public Guid CreatorId { get;  set; }
        public string Name { get;  set; }
        public string? Description { get;  set; }
        public bool Active { get;  set; }
    }
}
