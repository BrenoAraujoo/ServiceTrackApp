using ServiceTrackHub.Domain.Interfaces;

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
        public User(){}


        public User(string name,  string email, string passwordHash, string phone)
        {
            Name = name;
            Tasks = new List<Tasks>();
            Email = email;
            PasswordHash = passwordHash;
            Phone = phone;
            Active = true;
        }

        public void Activate()
        {
            Active = true;
            base.Update();
            
        }

        public void Deactivate()
        {
            base.Update();
            Active = false;
        }

        public void ChangePassword(string newPassword)
        {
            PasswordHash = newPassword;
        }
    }
}
