using Microsoft.AspNetCore.Mvc;
using Mini_Sonic.Model;
using Mini_Sonic.Service;
using Mini_Sonic_DAL.Contacts;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mini_Sonic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(IRepository<Categoty> categoryRepository)
        {
            _categoryService = new CategoryService(categoryRepository);
        }

        [HttpGet]
        public IEnumerable<Categoty> Get()
        {
            return _categoryService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Categoty> Get(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public ActionResult<Categoty> Post(Categoty category)
        {
            var newCategory = _categoryService.Add(category);
            return Ok( newCategory);
        }

        [HttpPut]
        public ActionResult<Categoty> Put(Categoty category)
        {
            try
            {
                var updatedCategory = _categoryService.Update(category);
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                _categoryService.Delete(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}

