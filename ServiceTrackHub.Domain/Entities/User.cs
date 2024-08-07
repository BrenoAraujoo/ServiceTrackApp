namespace ServiceTrackHub.Domain.Entities
{
    public sealed class User
    {
        public Guid Id { get;  private set; }
        public string Name { get;  private set; }
        public List<Tasks> Tasks { get;  private set; }

        public User(){}

        public User(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Tasks = new List<Tasks>();
        }
    }
}
