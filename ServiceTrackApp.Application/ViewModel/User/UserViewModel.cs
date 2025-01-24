namespace ServiceTrackApp.Application.ViewModel.User
{
    public record UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string SmartPhoneNumber { get; set; }
        public string? JobPosition { get; set; }
        public bool Active { get; set; }
        public string Role { get; set; }

        public static UserViewModel ToViewModel(Domain.Entities.User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email.Value,
                SmartPhoneNumber = user.SmartPhoneNumber,
                JobPosition = user.JobPosition.Value,
                Active = user.Active,
                Role = user.Role.ToString()
                
            };
        }

        public static List<UserViewModel> ToViewModel(IEnumerable<Domain.Entities.User> users)
        {
            return users.Select(ToViewModel).ToList();
        }

    }
}
