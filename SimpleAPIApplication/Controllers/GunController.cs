using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleAPIApplication.Models;

namespace SimpleAPIApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GunController : ControllerBase
    {
        public static List<Guns> Guns { get; set; }=new List<Guns>();

        [HttpGet]
        [Authorize(Roles ="Customer,Dealer")]
        public IEnumerable<Guns> GetUsers()
        {
            return Guns;
        }

        [HttpPost]
        [Authorize(Roles ="Dealer")]
        public async Task<IActionResult> PostUser(Guns gun)
        {
            if (gun == null)
            {
                return NoContent();

            }



            Guns.Add(gun);

            return  Ok(Guns);


        }
    }
}
