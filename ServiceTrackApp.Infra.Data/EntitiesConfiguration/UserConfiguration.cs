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
            
            builder.Property(x => x.RefreshTokenHash)
                .HasMaxLength(100);
            
            builder.Property(x => x.Id)
                .ValueGeneratedNever();
            
            // Configurar a conversão do enum Role para ser armazenado como int
            builder.Property(u => u.Role)
                .HasConversion<byte>()
                .IsRequired();

            // Configuracao de Value Objects
            builder.OwnsOne(x => x.PasswordHash, password =>
            {
                password.Property(x => x.Value)
                    .HasMaxLength(100)
                    .IsRequired()
                    .HasColumnName("PasswordHash");
            });
            
            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(x => x.Value)
                    .HasMaxLength(40)
                    .IsRequired()
                    .HasColumnName("Email");
            });
            
            builder.OwnsOne(x => x.SmartPhoneNumber, smartPhoneNumber =>
            {
                smartPhoneNumber.Property(x => x.Value)
                    .HasMaxLength(11)
                    .IsRequired(false)
                    .HasColumnName("SmartPhoneNumber");
            });

            builder.OwnsOne(x => x.JobPosition, jobPosition =>
            {
                jobPosition.Property(x => x.Value)
                    .HasMaxLength(40)
                    .IsRequired(false)
                    .HasColumnName("JobPosition");
            });
            
            builder
                .HasMany(x => x.Tasks)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Restrict);
                
        }
    }
}
