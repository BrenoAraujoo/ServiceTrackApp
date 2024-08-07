﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceTrackHub.Infra.Data.Context;

#nullable disable

namespace ServiceTrackHub.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ServiceTrackHub.Domain.Entities.TaskType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("task_type", (string)null);
                });

            modelBuilder.Entity("ServiceTrackHub.Domain.Entities.Tasks", b =>
                {
                    b.Property<Guid>("TaskId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("TaskTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserToId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TaskId");

                    b.HasIndex("TaskTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("tasks", (string)null);
                });

            modelBuilder.Entity("ServiceTrackHub.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("ServiceTrackHub.Domain.Entities.TaskType", b =>
                {
                    b.HasOne("ServiceTrackHub.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ServiceTrackHub.Domain.Entities.Tasks", b =>
                {
                    b.HasOne("ServiceTrackHub.Domain.Entities.TaskType", "TaskType")
                        .WithMany()
                        .HasForeignKey("TaskTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceTrackHub.Domain.Entities.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TaskType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ServiceTrackHub.Domain.Entities.User", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
