using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mini_Sonic.Model;
using Mini_Sonic.Service;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;


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
        public ActionResult<IEnumerable<Categoty>> Get()
        {
            try
            {
                var user = _userService.GetUser(CurrentUser.Email, CurrentUser.Password);
                Console.WriteLine(user);
                var categories = _categoryService.GetAll();
                return Ok( categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }


        }

        [HttpGet("current")]
        public ActionResult<Categoty> GetCurrentUserCategory()
        {
            try
            {
                var id = CurrentUser.Id;
                var category = _categoryService.GetUserCategory(id);


                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }

        }
        [HttpPost]
        public ActionResult<Categoty> Post(Categoty category)
        {
            try
            {
                category.userId = CurrentUser.Id;
                var newCategory = _categoryService.Add(category);
                return Ok(newCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
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

