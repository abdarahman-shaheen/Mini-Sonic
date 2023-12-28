using Microsoft.AspNetCore.Mvc;
using Mini_Sonic.Model;
using Mini_Sonic.Service;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mini_Sonic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        // Inject CategoryService through constructor
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public ActionResult<List<Category>> Get()
        {
            var categories = _categoryService.GetAllCategories();

            return Ok(categories);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public ActionResult<string> Post(Category category)
        {
            try
            {
                var categorys = _categoryService.AddCategory(category);

                return Ok(categorys);

            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut()]
        public ActionResult<string> Put(Category category)
        {
            try
            {
                var categoryupdate = _categoryService.UpdateCategory(category);
                return Ok(categoryupdate);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
