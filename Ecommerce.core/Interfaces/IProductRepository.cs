using Ecommerce.core.DTOs.Requests;
using Ecommerce.core.Entites.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> AddAsync(CreateProductDTO product);
        Task<bool> UpdateAsync(int id, CreateProductDTO product);
        Task DeleteAsync(int id);
    }
}
