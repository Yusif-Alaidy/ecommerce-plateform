using AutoMapper;
using Ecommerce.core.DTOs.Requests;
using Ecommerce.core.Entites.Product;
using Ecommerce.core.Interfaces;
using Ecommerce.core.Service_Interfaces;
using Ecommerce.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.infrastructure.Repositries
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IMapper mapper;
        private readonly AppDbContext context;
        private readonly IImageManagementService imageManagementService;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
        }

        public async Task<bool> AddAsync(CreateProductDTO productDTO)
        {
            if (productDTO == null)
                return false;

            var product = mapper.Map<Product>(productDTO)
                ;
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            // Add Images
            var ImagePath = await imageManagementService.AddImageAsync(productDTO.Photos, productDTO.Name);
            var photos = ImagePath.Select(e => new Photos
            {
                ImageName = e,
                ProductId = product.Id
            });
            await context.Photos.AddRangeAsync(photos);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
