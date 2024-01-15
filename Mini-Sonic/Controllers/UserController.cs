using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mini_Sonic.Model;
using Mini_Sonic.Service;
using Mini_Sonic_DAL.Contacts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Mini_Sonic.Controllers
{
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
        public ActionResult Get(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }

        [AllowAnonymous]
        // POST api/<UserController>
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var User = _userService.GetUser(user.Email,user.Password);

            if (User!=null)
            {
                if (User.Email == user.Email && User.Password == user.Password)
                {

                    User.Token = GenerateJwtToken(User);




                    return Ok(User);

                }

            }

            return Ok(new { error = "The email or password is not correct" });
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);

            var userJson = JsonSerializer.Serialize(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
        {
            new Claim("User", userJson, ClaimValueTypes.String) ,
             new Claim(ClaimTypes.Role, user.Role) 

            // Custom claim "User" stores the serialized user objectQ
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private User DecodeJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var serializedUser = claimsPrincipal.FindFirst("User").Value;
                return JsonSerializer.Deserialize<User>(serializedUser);
            }
            catch (Exception ex)
            {
                // Token validation failed
                return null;
            }

        }

        [HttpGet("profile")]
        public ActionResult<User> GetProfile()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                //if (string.IsNullOrEmpty(token))
                //{
                //    return BadRequest(new { error = "Authorization token is missing." });
                //}

               var user = DecodeJwtToken(token);

                //if (user == null)
                //{
                //    return Unauthorized(new { error = "Invalid or expired token." });
                //}
             

                return Ok(user);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
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
