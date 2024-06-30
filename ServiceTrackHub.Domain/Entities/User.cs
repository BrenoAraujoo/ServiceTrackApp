namespace ServiceTrackHub.Domain.Entities
{
    public sealed class User
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public ICollection<Service> Services { get;  set; }

    }
}
