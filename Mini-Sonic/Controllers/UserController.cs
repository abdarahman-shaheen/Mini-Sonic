using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mini_Sonic.Model;
using Mini_Sonic.Service;
using Mini_Sonic_DAL.Contacts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mini_Sonic.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IRepository<User> userRepository, IConfiguration configuration)
        {
            _userService = new UserService(userRepository);
            _configuration = configuration;
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

        [AllowAnonymous]
        // POST api/<UserController>
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var users = _userService.GetAll();
            foreach (var User in users)
            {
                if (User.Email == user.Email && User.Password == user.Password)
                {
                    

                    var newUser = new User {
                        Id = User.Id,
                        UserName = User.UserName,
                        Email = user.Email,
                        Password = user.Password,
                        Role = user.Role,
                        Token = GenerateJwtToken(user)
                };



                    return Ok(newUser);

                }

            }
            return Ok( new { error = "The email or password is not correct" });
            // If credentials are valid, generate JWT token

        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role) // Include user role
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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
