using Ecommerce.core.Entites.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.infrastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(30);
            builder.Property(e => e.Id).IsRequired();
            builder.HasData(new Category
            {
                Id = 1,
                Name = "Category 1",
            });
        }
    }
}
