using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(x => x.SupplierId).IsRequired();
        builder.Property(x => x.OrderDate).IsRequired();
        builder.Property(x => x.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.ExpectedDeliveryDate).IsRequired();
        builder.Property(x => x.OrderType).IsRequired();
        builder.Property(x => x.ReceivedDate).IsRequired();
    }
}