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
        public string Phone { get; private set; }
        public bool Active { get; private set; }
        public Role Role { get; private set; } //EF nav property
        protected User(){} // ORM constructor


        public User(string name,  string email, string passwordHash, string phone)
        {
            Name = name;
            Tasks = new List<Tasks>();
            Email = new Email(email).Value;
            PasswordHash = passwordHash;
            Phone = new Phone(phone).Value;
            Active = true;
        }

        public void Activate()
        {
            if (Active)
                throw new InvalidOperationException(ErrorMessage.UserIsAlreadyActivated);
            Active = true;
            base.Update();
            
        }

        public void Deactivate()
        {
            if (!Active)
                throw new InvalidOperationException(ErrorMessage.UserIsAlreadyInactivated);
            base.Update();
            Active = false;
        }

        public void ChangePassword(string newPassword)
        {
            PasswordHash = newPassword;
        }
    }
}
