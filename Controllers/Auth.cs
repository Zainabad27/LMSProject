using LmsApp2.Api.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        [HttpPost("Login/Admin")]
        public async Task<IActionResult> Login([FromBody]LoginDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }






        }
    }
}
