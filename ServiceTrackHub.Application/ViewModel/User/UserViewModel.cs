namespace ServiceTrackHub.Application.ViewModel.User
{
    public record UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }
}
