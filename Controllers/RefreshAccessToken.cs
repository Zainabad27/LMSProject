using LmsApp2.Api.DTOs;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RefreshAccessToken(ITokenServices TokenService) : ControllerBase
    {

        [HttpPost("{designation}")]
        public async Task<IActionResult> RefreshAccesstoken([FromBody] RefreshAccessTokenDto RefreshTokenData,[FromRoute] string designation)
        {

            var context = HttpContext;
            await TokenService.RefreshAccesToken(RefreshTokenData, context,designation);



            return Ok("Access Token Refreshed Successfully");
        }
    }
}
