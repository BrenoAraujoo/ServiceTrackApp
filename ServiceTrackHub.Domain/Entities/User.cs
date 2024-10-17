using ServiceTrackHub.Domain.Enums;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Interfaces;
using ServiceTrackHub.Domain.ValueObjects;

namespace ServiceTrackHub.Domain.Entities
{
    public sealed class User : BaseEntity, IEntityActivable
    {
        public string Name { get; private set; }
        public IReadOnlyCollection<Tasks> Tasks { get; private set; } = new List<Tasks>();
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string? RefreshTokenHash { get; private set; }
        public DateTime? RefreshTokenExpiresAt { get; private set; }
        public string? SmartPhoneNumber { get; private set; }
        public bool Active { get; private set; }
        public string? JobPosition { get; private set; }
        public Role Role { get; private set; } // EF navigation property

        // ORM constructor
        public User() { }

        public User(string name, string email, string password, string userRole, 
            string? jobPosition = null, string? smartPhoneNumber = null)
        {
            SetName(name);
            Email = new Email(email).Value;
            PasswordHash = new Password(password).Value;
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
            Active = false;
            base.Update();
        }

        public void Update(
            string? name = null,
            string? email = null,
            string? smartPhoneNumber = null,
            string? jobPosition = null,
            string? userRole = null)
        {
            if (name != null) UpdateName(name);
            if (email != null) UpdateEmail(email);
            if (smartPhoneNumber != null) UpdateSmartPhoneNumber(smartPhoneNumber);
            if (jobPosition != null) UpdateJobPosition(jobPosition);
            if (userRole != null) SetUserRole(userRole);
            base.Update();
        }

        public void SetPassword(string newPassword)
        {
            PasswordHash = newPassword;
        }

        public void UpdateRefreshToken(string newRefreshToken)
        {
            RefreshTokenHash = newRefreshToken;
        }

        public void UpdateRefreshTokenExpiresAt(DateTime? newRefreshTokenExpiresAt)
        {
            if(newRefreshTokenExpiresAt == null || newRefreshTokenExpiresAt < DateTime.Now)
                throw new InvalidOperationException("A data de expiração do Refresh Token está inválida");
            RefreshTokenExpiresAt = newRefreshTokenExpiresAt;
        }

        private void SetUserRole(string role)
        {
            if (!Enum.TryParse<Role>(role, out var roleEnum))
                throw new ArgumentException(ErrorMessage.UserRoleNotFound);

            Role = roleEnum;
        }

        public void UpdateEmail(string newEmail)
        {
            Email = new Email(newEmail).Value;
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

        private void UpdateJobPosition(string? jobPosition)
        {
            JobPosition = new JobPosition(jobPosition).Value;
        }
    }
}
