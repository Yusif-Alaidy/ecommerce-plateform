using AutoMapper;
using Ecommerce.core.DTOs.Requests;
using Ecommerce.core.Entites.Product;
using Ecommerce.core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ecommerce.api.Controllers
{

    public class CategoriesController : BaseController
    {

        #region Constructor
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        #endregion

        #region Get All Categories
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            try
            {

                var categories = await unitOfWork.CategoryRepository.GetAllAsync();
                if (categories == null)
                {
                    return BadRequest(new { message = "You Have no Categories" });
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Get By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                    return BadRequest();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        #endregion

        #region Create Category
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryRequest request)
        {
            try
            {
    
                var category = mapper.Map<Category>(request);

                await unitOfWork.CategoryRepository.AddAsync(category);

                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id,CategoryUpdate request)
        {
            try
            {
                var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                    return BadRequest();
                category.Name = request.Name;
                category.Description = request.Description;
                await unitOfWork.CategoryRepository.UpdateAsync(category);
                return Ok(category);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
                if(category == null)
                    return BadRequest();
                await unitOfWork.CategoryRepository.DeleteAsync(id);
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
