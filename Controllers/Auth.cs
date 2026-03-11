using System.Security.Claims;
using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class Auth(ILogin_Register AuthService) : ControllerBase
    {
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {

            var Id = User.FindFirstValue("MainTableId") ?? throw new CustomException("Unauthorized Access.", 403);

            Guid UserId = Guid.Parse(Id);

            await AuthService.Logout(HttpContext, UserId);

            return Ok("Logged out successfully.");

        }
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {

            var UserRole = User.FindFirstValue(ClaimTypes.Role) ?? throw new CustomException("Unauthorized Access, please Login", 403);
            var Id = User.FindFirstValue("MainTableId") ?? throw new CustomException("Unauthorized Access.", 403);

            return Ok(new ReturnUserDto(UserRole, Id));
        }


        [HttpPost("Login/{Role}")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto request, [FromRoute] string Role)
        {
            if (Role == "admin")
            {

                await AuthService.AdminLogin(request, HttpContext);

                return Ok("Admin Logged in successfully.");

            }
            else if (Role == "teacher")
            {
                await AuthService.TeacherLogin(request, HttpContext);

                return Ok("Teacher Logged in successfully.");
            }
            else if (Role == "student")
            {
                await AuthService.StudentLogin(request, HttpContext);

                return Ok("Student Logged in successfully.");
            }
            else
            {
                return BadRequest("Invalid Role specified for login.");
            }

        }

        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        [HttpPost("RegisterEmployee/{Designation}")]
        public async Task<IActionResult> AddEmployee([FromForm] EmployeeDto emp, [FromRoute] string Designation)
        {
            if (Designation != "Admin" && Designation != "Teacher") return BadRequest("Invalid Designation for Employee Registeration.");

            Guid addedEmployeeId = await AuthService.RegisterEmployee(emp, Designation);


            return Ok(new { AddedEmployeeId = addedEmployeeId });

        }

        [HttpPost("RegisterStudent")]
        [Consumes("multipart/form-data")]
        [AllowAnonymous]
        public async Task<IActionResult> AddStudent([FromForm] StudentDto stdData)
        {
            Guid StudentId = await AuthService.RegisterStudent(stdData);

            return Ok(new { AddedStudentId = StudentId });

        }
    }
}
