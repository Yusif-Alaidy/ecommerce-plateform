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
    }
}
