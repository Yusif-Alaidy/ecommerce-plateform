using AutoMapper;
using Ecommerce.core.DTOs.Requests;
using Ecommerce.core.DTOs.Resposnes;
using Ecommerce.core.Entites.Product;
using Ecommerce.core.Interfaces;
using Ecommerce.core.Service_Interfaces;
using Ecommerce.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.infrastructure.Repositries
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        #region Fields & Constructore
        private readonly IMapper mapper;
        private readonly AppDbContext context;
        private readonly IImageManagementService imageManagementService;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
        }
        #endregion

        #region Get add overload
        public async Task<ProductFilterd<ProductsResponse>> GetAllAsync(ProductQueryParams p)
        {
            // نبدأ بـ IQueryable — لسه مفيش SQL
            var query = context.Products
                .Include(x => x.Category)
                .Include(e => e.Photos)
                .AsNoTracking()
                .AsQueryable();

            // ── Search ──────────────────────────────
            if (!string.IsNullOrWhiteSpace(p.Search))
            {
                var term = p.Search.Trim().ToLower();
                query = query.Where(x =>
                    x.Name.ToLower().Contains(term) ||
                    x.Description.ToLower().Contains(term));
            }

            // ── Filters ──────────────────────────────
            if (p.CategoryId.HasValue)
                query = query.Where(x => x.CategoryId == p.CategoryId.Value);

            if (p.MinPrice.HasValue)
                query = query.Where(x => x.Price >= p.MinPrice.Value);

            if (p.MaxPrice.HasValue)
                query = query.Where(x => x.Price <= p.MaxPrice.Value);

            // ── Count قبل الـ pagination ──────────────
            var totalCount = await query.CountAsync();

            // ── Sorting ──────────────────────────────
            query = (p.SortBy.ToLower(), p.SortDir.ToLower()) switch
            {
                ("price", "asc") => query.OrderBy(x => x.Price),
                ("price", "desc") => query.OrderByDescending(x => x.Price),
                (_, "desc") => query.OrderByDescending(x => x.Name),
                _ => query.OrderBy(x => x.Name),   // default
            };

            // ── Pagination ────────────────────────────
            var items = await query
                .Skip((p.Page - 1) * p.PageSize)
                .Take(p.PageSize)
                .ToListAsync();   // هنا بس بيتبعت الـ SQL للـ DB

            var productDto = mapper.Map<List<ProductsResponse>>(items);


            return new ProductFilterd<ProductsResponse>
            {
                Items = productDto,
                TotalCount = totalCount,
                Page = p.Page,
                PageSize = p.PageSize
            };
        }
        #endregion

        #region Add Overloding
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
        #endregion

        #region Update Overloding
        public async Task<bool> UpdateAsync(int id, CreateProductDTO request)
        {
            var product = await context.Products.Include(e=>e.Category).Include(e=>e.Photos).FirstOrDefaultAsync(e=>e.Id==id);
            if (product == null)
                return false;

            mapper.Map(request,product);
            
            // Photos
            var FindPhotos = await context.Photos.Where(e=>e.ProductId == id).ToListAsync();
            foreach(var photo in FindPhotos)
            {
                imageManagementService.DeleteImageAsync(photo.ImageName);
            }
            context.Photos.RemoveRange(FindPhotos);

            var ImagePath = await imageManagementService.AddImageAsync(request.Photos, request.Name);
            var photos = ImagePath.Select(path => new Photos
            {
                ImageName = path,
                ProductId = product.Id
            });
            await context.Photos.AddRangeAsync(photos);

            await context.SaveChangesAsync();
            return true;


        }
        #endregion

        #region Delete Overreding
        public async Task DeleteAsync(int id)
        {
            var product = await context.Products.FindAsync(id);
            var Photos = await context.Photos.Where(e=>e.ProductId==id).ToListAsync();


            foreach (var photo in Photos)
            {
                imageManagementService.DeleteImageAsync(photo.ImageName);
            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();

        }
        

        #endregion
    }
}
