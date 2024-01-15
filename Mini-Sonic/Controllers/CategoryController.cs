using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mini_Sonic.Model;
using Mini_Sonic.Service;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mini_Sonic.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly CategoryService _categoryService;
        protected readonly IConfiguration _configuration;
        private readonly UserService _userService;

        public CategoryController(IRepository<Categoty> categoryRepository, UserService userService, IConfiguration configuration):base(configuration)
        {
            _categoryService = new CategoryService(categoryRepository);
            _configuration = configuration;
            _userService = userService;
        }

        [HttpGet]
        public IEnumerable<Categoty> Get()
        {
            var user = _userService.GetUser(CurrentUser.Email, CurrentUser.Password);
            Console.WriteLine(user);
            return _categoryService.GetAll();


        }

        [HttpGet("current")]
        public ActionResult<Categoty> GetCurrentUserCategory()
        {
            var id = CurrentUser.Id;
            var category = _categoryService.GetUserCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
        [HttpPost]
        public ActionResult<Categoty> Post(Categoty category)
        {
            category.userId = CurrentUser.Id;
            var newCategory = _categoryService.Add(category);
            return Ok( newCategory);
        }

        [HttpPut]
        public ActionResult<Categoty> Put(Categoty category)
        {
            try
            {
                var updatedCategory = _categoryService.Update(category);
                updatedCategory = (Result)1;
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

