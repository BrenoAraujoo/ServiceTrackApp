using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceTrackApp.Domain.Entities;

namespace ServiceTrackApp.Infra.Data.EntitiesConfiguration
{
    public class TasksConfiguration : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.ToTable("tasks");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.HasOne(x => x.TaskType)
                .WithMany()
                .HasForeignKey(x => x.TaskTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x=> x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Property(x => x.Description)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(x => x.Status)
                .HasConversion<sbyte>()
                .IsRequired();
            
            builder.Property(x => x.Priority)
                .HasConversion<sbyte>()
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}
