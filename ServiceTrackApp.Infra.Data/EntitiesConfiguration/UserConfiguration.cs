using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceTrackApp.Domain.Entities;

namespace ServiceTrackApp.Infra.Data.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Primary Key
            builder.HasKey(x => x.Id);
            builder.ToTable("users");
            builder.Property(x => x.Name).HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.Email).HasMaxLength(40)
                .IsRequired();
            builder.Property(x => x.PasswordHash).HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.RefreshTokenHash).HasMaxLength(100);
            builder.Property(x => x.SmartPhoneNumber).HasMaxLength(11)
                .IsRequired(false);
            builder.Property(x => x.Id)
                .ValueGeneratedNever();
            
            // Configurar a conversão do enum Role para ser armazenado como int
            builder.Property(u => u.Role)
                .HasConversion<byte>()
                .IsRequired();

            builder.Property(u => u.JobPosition)
                .HasMaxLength(40)
                .IsRequired(false);
            
            
            builder
                .HasMany(x => x.Tasks)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Restrict);
                
        }
    }
}
