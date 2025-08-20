using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleAPIApplication.Models;
using System;

namespace SimpleAPIApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        public static List<User> Users = new List<User>
            {
                new User(1,"Sankar","sankar@gmail.com","Dealer")
            };

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return Users;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(User user)
        {
            if (user == null)
            {
                return NoContent();

            }



            Users.Add(user);

            return Ok(Users);
            
              
        }
    }
}
