using Ecommerce.core.Entites.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.core.DTOs.Resposnes
{
    public record ProductsResponse(string Name, string Description, decimal Price, CategoryResponse Category, List<PhotosResponse> Photos);

}
