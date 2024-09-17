using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Interfaces;
using ServiceTrackHub.Domain.ValueObjects;

namespace ServiceTrackHub.Domain.Entities
{
    public sealed class User : BaseEntity, IEntityActivable    
    {
        public string Name { get;  private set; }
        public List<Tasks> Tasks { get;  private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string SmartPhoneNumber { get; private set; }
        public bool Active { get; private set; }
        public string? JobPosition { get; private set; }
        public Role Role { get; private set; } //EF nav property
        protected User(){} // ORM constructor


        public User(string name,  string email, string passwordHash, string smartPhoneNumber, string? jobPosition = null)
        {
            Name = name;
            Tasks = [];
            Email = new Email(email).Value;
            PasswordHash = passwordHash;
            SmartPhoneNumber = new SmartPhoneNumber(smartPhoneNumber).Value;
            JobPosition = new JobPosition(jobPosition).Value;
            Active = true;
        }

        public void Activate()
        {
            if (Active)
                throw new InvalidOperationException(ErrorMessage.UserIsAlreadyActivated);
            Active = true;
            Update();
        }

        public void Deactivate()
        {
            if (!Active)
                throw new InvalidOperationException(ErrorMessage.UserIsAlreadyInactivated);
            Update();
            Active = false;
        }

        public void ChangePassword(string newPassword)
        {
            PasswordHash = newPassword;
        }

        public void Update(string? name = null, string? email = null,  string? smartPhoneNumber = null,
            string? jobPosition = null)
        {
            Name =  name?? Name;
            Email = new Email(email?? Email).Value;
            SmartPhoneNumber = new SmartPhoneNumber(smartPhoneNumber?? SmartPhoneNumber).Value;
            JobPosition = new JobPosition(jobPosition?? JobPosition).Value;
            base.Update();
        }
    }
}
