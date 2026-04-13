using AutoMapper;
using Ecommerce.core.DTOs.Requests;
using Ecommerce.core.DTOs.Resposnes;
using Ecommerce.core.Entites.Product;

namespace Ecommerce.api.Mapper
{
    public class ConfMapping:Profile
    {
        public ConfMapping()
        {
            CreateMap<CategoryRequest, Category>().ReverseMap();
            CreateMap<CategoryResponse, Category>().ReverseMap();
            CreateMap<PhotosResponse, Photos>().ReverseMap();
            CreateMap<ProductsResponse, Product>().ReverseMap();
            CreateMap<CreateProductDTO, Product>().ForMember(e => e.Photos, e => e.Ignore())
                .ReverseMap();
        }
    }
}
