using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI111.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        string key = "12345123451234512345123451234523";

        [HttpGet("Index")]
        [AllowAnonymous]
        public IResult Login(string username, string password)
        {
            if (username == password)
            {
                var st = new JwtSecurityToken
                (
                    "nf173",
                    "everyone",
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, "admin")
                    },
                    DateTime.Now,
                    DateTime.Now,
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        SecurityAlgorithms.HmacSha512Signature)
                );

                string token = new JwtSecurityTokenHandler().WriteToken(st);
                return Results.Ok(token);
            }
            return Results.Unauthorized();
        }
    }
}