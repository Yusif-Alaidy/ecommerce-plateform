using Ecommerce.core.Entites.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(30);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Price).HasColumnType("decimal(18,2)");
            builder.HasData(new Product
            {
                Id = 1,
                Name = "product1",
                Price = 0,
                CategoryId = 1,
            });
        }
    }
}
