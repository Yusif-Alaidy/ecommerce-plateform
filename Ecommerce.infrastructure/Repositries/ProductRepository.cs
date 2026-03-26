using Ecommerce.core.Entites.Product;
using Ecommerce.core.Interfaces;
using Ecommerce.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.infrastructure.Repositries
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
