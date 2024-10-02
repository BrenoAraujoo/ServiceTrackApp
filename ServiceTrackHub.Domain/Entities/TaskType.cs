using ServiceTrackHub.Domain.Common.Erros;
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

        public TaskType(Guid creatorId,string name, string? description)
        {
            Name = name;
            Description = description;
            CreatorId = creatorId;
            Activate();
        }

        public void Activate()
        {
            if (Active)
                throw new InvalidOperationException(ErrorMessage.TaskAlreadyActivated);
            Active = true;
            base.Update();
        }

        public void Deactivate()
        {
            if (!Active)
                throw new InvalidOperationException(ErrorMessage.TaskAlreadyInactivated);
            Active = false;
            base.Update();
        }

        public void Update(string? name, string? description)
        {
            Name = name ?? Name;
            Description = description ?? Description;
            base.Update();
        }
    }
}
