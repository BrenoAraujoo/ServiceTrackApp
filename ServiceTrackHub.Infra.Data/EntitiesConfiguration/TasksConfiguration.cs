﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceTrackHub.Domain.Enums.Entities;

namespace ServiceTrackHub.Infra.Data.EntitiesConfiguration
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
                .HasForeignKey(x => x.TaskTypeId);

            builder.Property(x => x.Description)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
