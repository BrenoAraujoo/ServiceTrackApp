namespace ServiceTrackHub.Domain.Entities
{
    public class TaskType
    {
        public Guid Id { get; private set; }
        public Guid CreatorId { get; private set; }
        public User User{ get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }

        public TaskType(){}

        public TaskType(string name, string? description, Guid creatorId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            CreatorId = creatorId;
        }
    }
}
