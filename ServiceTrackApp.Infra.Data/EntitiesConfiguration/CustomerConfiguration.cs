using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceTrackApp.Domain.Entities;

namespace ServiceTrackApp.Infra.Data.EntitiesConfiguration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {

        builder.ToTable("customer")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.HasMany(c => c.Tasks)
            .WithOne(t => t.Customer)
            .HasForeignKey(t => t.CustomerId);
        
        //Value objects
        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(x => x.Street).IsRequired().HasMaxLength(100).HasColumnName("Street");
            address.Property(x => x.City).IsRequired().HasMaxLength(40).HasColumnName("City");
            address.Property(x => x.Country).IsRequired().HasMaxLength(30).HasColumnName("Country");
            address.Property(x => x.PostalCode).IsRequired().HasMaxLength(8).HasColumnName("PostalCode");
            address.Property(x => x.State).IsRequired().HasMaxLength(30).HasColumnName("State");
        });

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Email");
        });
        
        builder.OwnsOne(x => x.SmartPhoneNumber, phone =>
        {
            phone.Property(x => x.Value)
                .IsRequired(false)
                .HasMaxLength(11)
                .HasColumnName("SmartPhoneNumber");
        });

    }
}