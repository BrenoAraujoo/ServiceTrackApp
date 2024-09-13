namespace ServiceTrackHub.Application.ViewModel.Task
{
    public record TasksViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public Guid UserToId { get; set; }
        public Guid TaskTypeId { get; set; }
        public DateTime CreationDate { get;  set; }
        public DateTime UpdateDate{ get;  set; }
        
    }
}
