using Ecommerce.core.Entites.Product;
using Ecommerce.core.Interfaces;
using Ecommerce.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.infrastructure.Repositries
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
