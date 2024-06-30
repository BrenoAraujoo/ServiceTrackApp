using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Infra.Data.EntitiesConfiguration
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100).IsRequired();

            builder.HasOne(x => x.User).WithMany(x => x.Services).HasForeignKey(x => x.UserId);
        }
    }
}
