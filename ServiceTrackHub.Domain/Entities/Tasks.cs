namespace ServiceTrackHub.Domain.Enums.Entities
{
    public sealed class Tasks : BaseEntity
    {
        public Guid TaskTypeId{ get; set; }
        public TaskType TaskType { get; set; }
        public Guid UserId { get; private set; }
       
        public Guid? UserToId{ get; private set; }
        public string? Description { get; private set; }
        public User User { get; private set; } // EF nav property
        protected Tasks(){}

        public Tasks(Guid taskTypeId,Guid userFromId, Guid? userToId, string? description)
        {
            TaskTypeId = taskTypeId;
            UserId = userFromId;
            UserToId = userToId;
            Description = description;
        }
    }
}
