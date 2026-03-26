using Ecommerce.core.Interfaces;
using Ecommerce.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.infrastructure.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public ICategoryRepository CategoryRepository { get; }
         
        public IProductRepository ProductRepository {  get; }

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
            CategoryRepository = new CategoryRepository(context);
            ProductRepository = new ProductRepository(context);
        }
    }
}
