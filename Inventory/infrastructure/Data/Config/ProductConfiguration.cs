using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Description).HasMaxLength(200);
    
        builder.Property(x => x.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.TaxCost).HasColumnType("decimal(18,2)");
        builder.Property(x => x.ProfitPerUnit).HasColumnType("decimal(18,2)");
        builder.Property(x => x.ProductionCost).HasColumnType("decimal(18,2)");
        
    
        builder.HasOne(x => x.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    
        builder.HasOne(x => x.Inventory)
            .WithMany(i => i.Products)
            .HasForeignKey(x => x.InventoryId)
            .OnDelete(DeleteBehavior.Restrict);
    
        builder.HasOne(x => x.Transaction)
            .WithMany(t => t.Products)
            .HasForeignKey(x => x.TransactionId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasMany(x => x.OrderDetails)
            .WithOne(od => od.Product)
            .HasForeignKey(od => od.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}