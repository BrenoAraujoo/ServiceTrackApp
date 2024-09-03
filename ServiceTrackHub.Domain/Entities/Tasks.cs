namespace ServiceTrackHub.Domain.Entities
{
    public sealed class Tasks : BaseEntity
    {
        public Guid TaskTypeId{ get; set; }
        public TaskType TaskType { get; set; }
        public string  Name { get; set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public Guid? UserToId{ get; private set; }
        public string? Description { get; private set; }

        protected Tasks(){}

        public Tasks(Guid taskeTypeId,Guid userFromId, Guid? userToId, string name,string? description)
        {
            TaskTypeId = taskeTypeId;
            UserId = userFromId;
            UserToId = userToId;
            Name = name;
            Description = description;
        }
    }
}
