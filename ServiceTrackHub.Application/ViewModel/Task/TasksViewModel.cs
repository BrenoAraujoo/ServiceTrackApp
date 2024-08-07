namespace ServiceTrackHub.Application.ViewModel.Task
{
    public record TasksViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
