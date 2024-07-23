using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Infra.Data.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Primary Key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder
                .HasMany(x => x.Tasks)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Restrict);
                

        }
    }
}
