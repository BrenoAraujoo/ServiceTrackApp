using ServiceTrackHub.Domain.Interfaces;

namespace ServiceTrackHub.Domain.Entities
{
    public class TaskType : BaseEntity, IEntityActivable
    {

        public Guid CreatorId { get; private set; }

        public string Name { get; private set; }
        public string? Description { get; private set; }
        public bool Active { get; private set; }
        public User User{ get; private set; } //EF nav property
        public TaskType(){}

        public TaskType(string name, string? description, Guid creatorId)
        {
            Name = name;
            Description = description;
            CreatorId = creatorId;
            Activate();
        }

        public void Activate()
        {
            Active = true;
            base.Update();
        }

        public void Deactivate()
        {
            Active = false;
            base.Update();
        }
    }
}
