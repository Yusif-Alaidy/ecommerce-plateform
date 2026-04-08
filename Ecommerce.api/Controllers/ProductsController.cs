using AutoMapper;
using Ecommerce.core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.core.DTOs.Resposnes;

namespace Ecommerce.api.Controllers
{

    public class ProductsController : BaseController
    {
        #region Constructor
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        #endregion

        #region Get all Products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await unitOfWork.ProductRepository.GetAllAsync(e => e.Category, e => e.Photos);
                if (products == null) 
                    return NotFound();
                var productDTOs = mapper.Map<List<ProductsResponse>>(products);
                return Ok(productDTOs);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
