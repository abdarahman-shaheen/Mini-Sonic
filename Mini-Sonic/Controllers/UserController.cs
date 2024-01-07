using Microsoft.AspNetCore.Mvc;
using Mini_Sonic.Model;
using Mini_Sonic.Service;
using Mini_Sonic_DAL.Contacts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mini_Sonic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserService _userService;

        public UserController(IRepository<User> userRepository)
        {
            _userService = new UserService(userRepository);
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            try
            {
                var users = _userService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        private ActionResult HandleException(Exception ex)
        {
            // Log the exception or handle it accordingly
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
