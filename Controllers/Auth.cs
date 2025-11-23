using LmsApp2.Api.DTOs;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class Auth(ILoginService LoginService) : ControllerBase
    {
        [HttpPost("Login/Admin")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var context = HttpContext;

            int EmployeeId = await LoginService.AdminLogin(request, context);



            return Ok("Employee Logged in successfully.");

        }
    }
}
