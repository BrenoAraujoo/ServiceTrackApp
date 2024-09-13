namespace ServiceTrackHub.Application.ViewModel.User
{
    public record UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }

        public static UserViewModel ToViewModel(Domain.Entities.User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Phone = user.Phone,
                Active = user.Active,
            };
        }
    }
}
