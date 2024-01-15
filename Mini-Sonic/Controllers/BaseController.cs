using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Mini_Sonic.Model;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class BaseController : Controller
{
    protected readonly IConfiguration _configuration;
    protected User CurrentUser;

    public BaseController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        //base.OnActionExecuting(context);

        try
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                context.Result = BadRequest(new { error = "Authorization token is missing." });
                return;
            }

            var user = DecodeJwtToken(token);

            if (user == null)
            {
                context.Result = Unauthorized(new { error = "Invalid or expired token." });
                return;
            }

            // Optionally, you can check if the user has the required role.
            // For example, you can check if the user has an "Admin" role:
          

            CurrentUser = (User)user;
        }
        catch (Exception ex)
        {
            // Log the exception or handle it accordingly
            throw new Exception("error");
        }
    }

    private object DecodeJwtToken(string token)
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
}
