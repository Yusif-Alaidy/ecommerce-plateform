using AutoMapper;
using Ecommerce.core.DTOs.Requests;
using Ecommerce.core.Entites.Product;

namespace Ecommerce.api.Mapper
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryRequest, Category>().ReverseMap();
        }
    }
}
