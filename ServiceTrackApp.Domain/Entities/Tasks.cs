using ServiceTrackApp.Domain.Common.Erros;
using ServiceTrackApp.Domain.Enums;
using ServiceTrackApp.Domain.ValueObjects;

namespace ServiceTrackApp.Domain.Entities
{
    public sealed class Tasks : BaseEntity
    {
        
        private const int MinDescriptionLength = 3;
        private const int MaxDescriptionLength = 100;
        public Guid TaskTypeId{ get; private set; }
        public TaskType TaskType { get; private set; }
        public Guid UserId { get; private set; }
        public Guid? UserToId{ get; private  set; }
        public string? Description { get; private set; }
        public Priority Priority { get; private set; }
        public Status Status { get; private set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; private set; } // Ef nav property
        public User User { get; private set; } // EF nav property
        protected Tasks(){}

        public Tasks(
            Guid taskTypeId,
            Guid userFromId,
            Guid? userToId, 
            string? description, 
            bool isUserActive,
            Priority priority,
            Status status,
            Guid customerId)
        {
            TaskTypeId = taskTypeId;
            UserId = userFromId;
            SetUserToId(userToId, isUserActive);
            SetDescription(description);
            Priority = priority;
            Status = status;
            CustomerId = customerId;
        }

        public void Update(Guid? userToId, bool isUserActive, string? description)
        {
            SetUserToId(userToId, isUserActive);
            SetDescription(description);
            base.Update();
        }

        private void SetDescription(string? description)
        {
            if (!string.IsNullOrWhiteSpace(description))
            {
                switch (description.Length)
                {
                    case > MaxDescriptionLength:
                        throw new ArgumentException(ErrorMessage.TaskMaxDescription);
                    case < MinDescriptionLength:
                        throw new ArgumentException(ErrorMessage.TaskMinDescription);
                }
            }
            Description = description ?? Description;
        }

        private void SetUserToId(Guid? userId, bool isUserActive)
        {
            if(userId is not null  && !isUserActive)
                throw new InvalidOperationException(ErrorMessage.TaskInvalidUser);
            UserToId = userId ?? UserToId;
        }
    }
}
