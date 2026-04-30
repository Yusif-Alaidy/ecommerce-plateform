using AutoMapper;
using Ecommerce.core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.core.DTOs.Resposnes;
using Ecommerce.core.DTOs.Requests;

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
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParams request)
        {
            try
            {
                var products = await unitOfWork.ProductRepository.GetAllAsync(request);
                if (products == null) 
                    return NotFound();
                //var productDTOs = mapper.Map<List<ProductsResponse>>(products);
                //return Ok(productDTOs);
                return Ok(products);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Get Product by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            try
            {
                var product = await unitOfWork.ProductRepository.GetByIdAsync(id, e => e.Category, e=>e.Photos);
                if (product == null)
                    return NotFound();

                var productDTO = mapper.Map<ProductsResponse>(product);
                return Ok(productDTO);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Create Product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm]CreateProductDTO request)
        {
            try
            {
                var product = await unitOfWork.ProductRepository.AddAsync(request);
                return Ok(new {msh = "the products created succefully"});
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Update Product
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int Id, CreateProductDTO request) 
        {
            try
            {

                var product = await unitOfWork.ProductRepository.UpdateAsync(Id, request);
                return Ok(new {msg = "Updated Successfully"});
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Delete Product
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await unitOfWork.ProductRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        #endregion
    }
}
