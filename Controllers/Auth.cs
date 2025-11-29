using LmsApp2.Api.DTOs;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class Auth(ILoginService LoginService) : ControllerBase
    {
        [HttpPost("Login/Admin")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var context = HttpContext;

            int EmployeeId = await LoginService.AdminLogin(request, context);



            return Ok("Employee Logged in successfully.");

        }
        [HttpPost("Login/Teacher")]
        public async Task<IActionResult> LoginTeacher([FromBody] LoginDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var context = HttpContext;

            int EmployeeId = await LoginService.TeacherLogin(request, context);



            return Ok("Employee Logged in successfully.");

        }
        [HttpPost("Login/Student")]
        public async Task<IActionResult> LoginStudent([FromBody] LoginDto request)
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
