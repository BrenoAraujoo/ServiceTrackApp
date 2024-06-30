namespace ServiceTrackHub.Domain.Entities
{
    public sealed class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int  UserId { get; set; }
        public User User { get; set; }
    }
}
