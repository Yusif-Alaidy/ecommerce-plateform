using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.core.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
    }
}
