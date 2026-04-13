using AutoMapper;
using Ecommerce.core.Interfaces;
using Ecommerce.core.Service_Interfaces;
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
        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;
        public UnitOfWork(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService)
        {
            this.context = context;
            CategoryRepository = new CategoryRepository(context);
            ProductRepository = new ProductRepository(context, mapper, imageManagementService);
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
        }
    }
}
