namespace ServiceTrackHub.Domain.Entities
{
    public sealed class Tasks : BaseEntity
    {
        public Guid TaskTypeId{ get; set; }
        public User TaskType { get; set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public Guid UserToId{ get; private set; }
        public string? Description { get; private set; }

        protected Tasks(){}

        public Tasks(Guid taskeTypeId, User taskeType, Guid userFromId, Guid userToId, string? description)
        {
            TaskTypeId = taskeTypeId;
            TaskType = taskeType;
            UserId = userFromId;
            UserToId = userToId;
            Description = description;
        }
    }
}
