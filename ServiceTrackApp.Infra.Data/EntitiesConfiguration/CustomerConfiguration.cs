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
            .IsRequired(false)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);
        
        //Value objects
        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(x => x.Street).IsRequired().HasMaxLength(100);
            address.Property(x => x.City).IsRequired().HasMaxLength(40);
            address.Property(x => x.Country).IsRequired().HasMaxLength(30);
            address.Property(x => x.PostalCode).IsRequired().HasMaxLength(8);
            address.Property(x => x.State).IsRequired().HasMaxLength(30);
        });

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(50);
        });
        
        builder.OwnsOne(x => x.SmartPhoneNumber, phone =>
        {
            phone.Property(x => x.Value)
                .IsRequired(false)
                .HasMaxLength(11);
        });

    }
}