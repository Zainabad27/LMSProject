using LmsApp2.Api.DTOs;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshAccessToken(ITokenServices TokenService) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> RefreshAccesstoken([FromBody] RefreshAccessTokenDto RefreshTokenData)
        {

            var context = HttpContext;
            Guid EmployeeId = await TokenService.RefreshAccesToken(RefreshTokenData, context);



            return Ok("Access Token Refreshed Successfully");
        }
    }
}
