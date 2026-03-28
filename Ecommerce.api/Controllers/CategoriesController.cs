using Ecommerce.core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.api.Controllers
{

    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region Get All Categories
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            try
            {

                var categories = await unitOfWork.CategoryRepository.GetAllAsync();
                if (categories == null)
                {
                    return BadRequest(new {message = "You Have no Categories"});
                }
                return Ok(categories);
            }
            catch(Exception ex)  
            { 
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Get By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                    return BadRequest(new { message = "This Category is not Exist");
                return Ok(category);
            }
            catch (Exception ex) 
            {
                return BadRequest($"{ex.Message}");
            }
        }
        #endregion
    }
}
