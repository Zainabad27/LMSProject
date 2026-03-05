using LmsApp2.Api.DTOs;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeServices) : ControllerBase
    {
        [HttpGet("GetAllCourses/{TeacherId}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetAllCourses([FromRoute] Guid TeacherId)
        {
            IEnumerable<SendCoursesToFrontendDto> courses = await employeeServices.GetAllCourses(TeacherId);

            return Ok(courses);
        }
        [HttpGet("GetEmployee/{EmployeeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] Guid EmployeeId)
        {
            var emp = await employeeServices.GetEmployeeById(EmployeeId);


            Console.WriteLine(emp);

            return Ok(emp);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllTeachers")]
        public async Task<IActionResult> GetAllTeachers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            Pagination<SendTeachersToFrontend> TeachersList = await employeeServices.GetAllTeachers(page, pageSize);

            return Ok(TeachersList);
        }

        [HttpPost("AssignCourse")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignCourseToATeacher([FromBody] AssignCourseDto assignCourse)
        {


            await employeeServices.AssignCourseToTeacher(assignCourse.TeacherId, assignCourse.CourseId);


            return Ok("Course Assigned Successfully");


        }
    }
}