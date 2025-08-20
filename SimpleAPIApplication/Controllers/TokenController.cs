using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleAPIApplication.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SimpleAPIApplication.Controllers;

namespace SimpleAPIApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;
        public TokenController(IConfiguration configuration) { 
                         
            _config=configuration;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(User userData)
        {
            if (userData != null && !string.IsNullOrEmpty(userData.Name))
            {
                var user = GetUser(userData.Name, userData.Roles);
                if (user != null)
                {
                  
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));
                    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                 
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Name!),
                        new Claim(ClaimTypes.Role, user.Roles!)
                    };

                
                    var tokenDescription = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.Now.AddDays(2),
                        SigningCredentials = cred,
                        Issuer = _config["Jwt:Issuer"],
                        Audience = _config["Jwt:Audience"]
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var myToken = tokenHandler.CreateToken(tokenDescription);
                    var token = tokenHandler.WriteToken(myToken);

                    return Ok(new { token });
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest("Invalid request data");
            }
        }

        private User? GetUser(string username, string role)
        {
            return UserController.Users.FirstOrDefault(u =>
                u.Name == username &&

                u.Roles == role);
        }
    }
}
