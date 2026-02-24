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

            var Id = User.FindFirstValue("MainTableId") ?? throw new CustomException("Unauthorized Access.",403);

            Guid UserId = Guid.Parse(Id);

            await AuthService.Logout(HttpContext, UserId);

            return Ok("Logged out successfully.");

        }
        [HttpPost("Login/Admin")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginDto request)
        {
            var context = HttpContext;

            await AuthService.AdminLogin(request, context);
            return Ok("Admin Logged in successfully.");

        }
        [HttpPost("Login/Teacher")]
        public async Task<IActionResult> LoginTeacher([FromBody] LoginDto request)
        {


            await AuthService.TeacherLogin(request, HttpContext);



            return Ok("Teacher Logged in successfully.");

        }
        [HttpPost("Login/Student")]
        public async Task<IActionResult> LoginStudent([FromBody] LoginDto request)
        {



            await AuthService.StudentLogin(request, HttpContext);

            return Ok("Student Logged in successfully.");




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
