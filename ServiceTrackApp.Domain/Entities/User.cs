using ServiceTrackApp.Domain.Common.Erros;
using ServiceTrackApp.Domain.Enums;
using ServiceTrackApp.Domain.Interfaces;
using ServiceTrackApp.Domain.ValueObjects;

namespace ServiceTrackApp.Domain.Entities
{
    public sealed class User : BaseEntity, IEntityActivable
    {
        public string Name { get; private set; }
        public IReadOnlyCollection<Tasks> Tasks { get; private set; } = new List<Tasks>();
        public Email Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string? RefreshTokenHash { get; private set; }
        public DateTime? RefreshTokenExpiresAt { get; private set; }
        public string? SmartPhoneNumber { get; private set; }
        public bool Active { get; private set; }
        public JobPosition? JobPosition { get; private set; }
        public Role Role { get; private set; } // EF navigation property

        // ORM constructor
        public User() { }

        public User(string name, Email email, string password, Role role, 
            JobPosition? jobPosition = null, string? smartPhoneNumber = null)
        {
            SetName(name);
            Email = email;
            PasswordHash = new Password(password).Value;
            SmartPhoneNumber = new SmartPhoneNumber(smartPhoneNumber).Value;
            SetUserRole(role);
            JobPosition = jobPosition;
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
            Active = false;
            base.Update();
        }

        public void Update(
            string? name = null,
            Email? email = null,
            string? smartPhoneNumber = null,
            JobPosition? jobPosition = null,
            Role? role = null)
        {
            if (name != null) UpdateName(name);
            if (email != null) UpdateEmail(email);
            if (smartPhoneNumber != null) UpdateSmartPhoneNumber(smartPhoneNumber);
            if (jobPosition != null) UpdateJobPosition(jobPosition);
            if (role.HasValue) SetUserRole(role.Value);
            base.Update();
        }

        public void SetPassword(string newPassword)
        {
            PasswordHash = newPassword;
        }

        public void UpdateRefreshToken(string? newRefreshToken)
        {
            RefreshTokenHash = newRefreshToken;
        }

        public void UpdateRefreshTokenExpiresAt(DateTime? newRefreshTokenExpiresAt)
        {
            if(newRefreshTokenExpiresAt == null || newRefreshTokenExpiresAt < DateTime.Now)
                throw new InvalidOperationException("A data de expiração do Refresh Token está inválida");
            RefreshTokenExpiresAt = newRefreshTokenExpiresAt;
        }

        private void SetUserRole(Role role)
        {
            if (!Enum.IsDefined(typeof(Role), role))
                throw new ArgumentException(ErrorMessage.UserRoleNotFound);
            Role = role;
        }

        private void UpdateEmail(Email newEmail)
        {
            Email = newEmail;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "O nome não pode ser vazio ou null");
            if (name.Length < 2)
                throw new ArgumentException("O nome precisa conter pelo menos 2 caracteres", nameof(name));
            Name = name;
        }

        public void UpdateName(string name)
        {
            SetName(name);
        }

        private void UpdateSmartPhoneNumber(string? smartPhoneNumber)
        {
            SmartPhoneNumber = new SmartPhoneNumber(smartPhoneNumber).Value;
        }

        private void UpdateJobPosition(JobPosition? jobPosition)
        {
            JobPosition = jobPosition;
        }
    }
}
