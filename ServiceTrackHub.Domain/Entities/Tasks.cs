namespace ServiceTrackHub.Domain.Entities
{
    public sealed class Tasks
    {
        public Guid TaskId { get; private set; }
        public Guid TaskTypeId{ get; set; }
        public TaskType TaskType { get; set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public Guid UserToId{ get; private set; }
        public string? Description { get; private set; }

        protected Tasks(){}

        public Tasks(Guid taskeTypeId, TaskType taskeType, Guid userFromId, Guid userToId, string? description)
        {
            TaskId = Guid.NewGuid();
            TaskTypeId = taskeTypeId;
            TaskType = taskeType;
            UserId = userFromId;
            UserToId = userToId;
            Description = description;
        }
    }
}
