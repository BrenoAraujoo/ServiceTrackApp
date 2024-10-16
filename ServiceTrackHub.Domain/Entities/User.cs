using ServiceTrackHub.Domain.Enums;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Interfaces;
using ServiceTrackHub.Domain.ValueObjects;

namespace ServiceTrackHub.Domain.Entities
{
    public sealed class User : BaseEntity, IEntityActivable    
    {
        public string Name { get;  private set; }
        public IEnumerable<Tasks> Tasks { get;  private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string? RefreshTokenHash { get; private set; }
        public string? SmartPhoneNumber { get; private set; }
        public bool Active { get; private set; }
        public string? JobPosition { get; private set; }
        
        public Role Role { get; private set; } //EF nav property
        protected User(){} // ORM constructor


        public User(string name,  string email, string password, string userRole, 
            string? jobPosition = null, string? smartPhoneNumber = null)
        {
            Name = name;
            Tasks = [];
            Email = new Email(email).Value;
            PasswordHash = new  Password(password).Value;
            SmartPhoneNumber = new SmartPhoneNumber(smartPhoneNumber).Value;
            SetUserRole(userRole);
            JobPosition = new JobPosition(jobPosition).Value;
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


        public void Update(
            string? name = null,
            string? email = null,
            string? smartPhoneNumber = null,
            string? jobPosition = null,
            string? userRole = null)
        {
            Name =  name?? Name;
            Email = new Email(email?? Email).Value;
            SmartPhoneNumber = new SmartPhoneNumber(smartPhoneNumber?? SmartPhoneNumber).Value;
            JobPosition = new JobPosition(jobPosition?? JobPosition).Value;
            SetUserRole(userRole);
            base.Update();
        }
        
        public void SetPassword(string newPassword)
        {
            PasswordHash = newPassword;
        }

        public void SetRefreshToken(string newRefreshToken)
        {
            RefreshTokenHash = newRefreshToken;
        }


        private void SetUserRole(string? role)
        {
            var existsRole = Enum.TryParse(role, out Role roleEnum);
            if (!existsRole)
                throw new ArgumentException(ErrorMessage.UserRoleNotFound);

            Role = roleEnum;
        }  
    }
}
