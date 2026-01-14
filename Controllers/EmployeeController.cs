using LmsApp2.Api.DTOs;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeServices) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        [HttpPost("AddAdmin")]
        //[HttpPost]
        public async Task<IActionResult> AddAdmin([FromForm] EmployeeDto emp)
        {
            var u = User;
            

            Console.WriteLine($"this is the employee Data:=====>>>> {emp}");


            Guid addedEmployeeId = await employeeServices.AddEmployee(emp, "Admin");

            var context = HttpContext;

            Console.WriteLine(context);
            return Ok(new { AddedEmployeeId = addedEmployeeId });

        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddTeacher")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddTeacher([FromForm] EmployeeDto emp)
        {




            Guid addedEmployeeId = await employeeServices.AddEmployee(emp, "Teacher");

           
            return Ok(new { AddedEmployeeId = addedEmployeeId });

        }



        [HttpPost("AssignCourse")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignCourseToATeacher([FromBody] AssignCourseDto assignCourse)
        {


            await employeeServices.AssignCourseToTeacher(assignCourse.TeacherId,assignCourse.CourseId);


            return Ok("Course Assigned Successfully");


        }
    }
}