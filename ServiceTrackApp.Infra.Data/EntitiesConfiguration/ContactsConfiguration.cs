using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceTrackApp.Domain.Entities;

namespace ServiceTrackApp.Infra.Data.EntitiesConfiguration;

public class ContactsConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("contacts");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        
        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.HasOne(x => x.Customer)
            .WithMany(c => c.Contacts)
            .HasForeignKey(x => x.CustomerId);

        //Configure Value Objects
        builder.OwnsOne(x => x.SmartPhoneNumber, smartPhoneNumberBuilder =>
        {
            smartPhoneNumberBuilder.Property(x => x.Value)
                .HasColumnName("SmartPhoneNumber")
                .IsRequired(false)
                .HasMaxLength(11);
        });
        
        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(x => x.Value)
                .HasColumnName("Email")
                .IsRequired(false)
                .HasMaxLength(50);
        });
        
        builder.OwnsOne(x => x.JobPosition, jobPosition =>
        {
            jobPosition.Property(x => x.Value)
                .HasColumnName("JobPosition")
                .IsRequired(false)
                .HasMaxLength(40);
        });
    }
}