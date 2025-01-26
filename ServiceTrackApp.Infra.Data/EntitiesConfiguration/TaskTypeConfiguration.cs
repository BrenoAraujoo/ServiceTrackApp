using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceTrackApp.Domain.Entities;

namespace ServiceTrackApp.Infra.Data.EntitiesConfiguration
{
    public class TaskTypeConfiguration : IEntityTypeConfiguration<TaskType>
    {
        public void Configure(EntityTypeBuilder<TaskType> builder)
        {
            builder.HasKey(t => t.Id);
            builder.ToTable("task_types");
            builder.Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(t => t.Description).HasMaxLength(100);
            builder.Property(t => t.Id).ValueGeneratedNever();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.CreatorId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
