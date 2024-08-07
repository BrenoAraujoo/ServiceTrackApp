namespace ServiceTrackHub.Domain.Entities
{
    public sealed class User
    {
        public Guid Id { get;  private set; }
        public string Name { get;  private set; }
        public List<Tasks> Tasks { get;  private set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public User(){}


        public User(string name,  string email, string password, string phone)
        {
            Id = Guid.NewGuid();
            Name = name;
            Tasks = new List<Tasks>();
            Email = email;
            Password = password;
            Phone = phone;
        }

    }
}
