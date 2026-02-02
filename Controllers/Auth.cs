using LmsApp2.Api.DTOs;
using LmsApp2.Api.Identity;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class Auth(ILogin_Register AuthService) : ControllerBase
    {
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
        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin([FromForm] EmployeeDto emp)
        {
            var u = User;

            Guid addedEmployeeId = await AuthService.RegisterEmployee(emp, "Admin");


            return Ok(new { AddedEmployeeId = addedEmployeeId });

        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddTeacher")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddTeacher([FromForm] EmployeeDto emp)
        {
            Guid addedEmployeeId = await AuthService.RegisterEmployee(emp, "Teacher");
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
