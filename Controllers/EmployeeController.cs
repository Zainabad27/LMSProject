using LmsApp2.Api.DTOs;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{ 
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeServices) : ControllerBase
    {
        [HttpGet("GetEmployee/{EmployeeId}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] Guid EmployeeId)
        {
            var emp = await employeeServices.GetEmployeeById(EmployeeId);

            return Ok(emp);
        }   

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllTeachers")] 
        public async Task<IActionResult> GetAllTeachers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            Pagination<SendTeachersToFrontend> TeachersList = await employeeServices.GetAllTeachers(page, pageSize);

            return Ok(TeachersList);    
            // throw new NotImplementedException();
        }   

        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin([FromForm] EmployeeDto emp)
        {
            var u = User;

            Guid addedEmployeeId = await employeeServices.AddEmployee(emp, "Admin");


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