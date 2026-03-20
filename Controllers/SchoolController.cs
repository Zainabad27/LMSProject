using System.Collections;
using LmsApp2.Api.DTOs;
using LmsApp2.Api.Services;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class SchoolController(ISchoolService SchoolServices,IEmployeeService empSerivce) : ControllerBase
    {
         [Authorize(Roles = "Admin")]
        [HttpGet("GetAllTeachers")]
        public async Task<IActionResult> GetAllTeachers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            Pagination<SendTeachersToFrontend> TeachersList = await empSerivce.GetAllTeachers(page, pageSize);

            return Ok(TeachersList);
        }
        [HttpPost("addschool")]
        public async Task<IActionResult> AddSchool([FromBody] SchoolDto School)
        {


            Guid AddedSchoolId = await SchoolServices.AddSchool(School);

            return Created("Somewhere", new { SchoolId = AddedSchoolId });
        }


        [HttpGet("GetAllCourses/{SchoolId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCourses([FromRoute] Guid SchoolId)
        {
            IEnumerable<SendCoursesToFrontendDto> AllCoursesOfThisSchool = await SchoolServices.GetAllCourses(SchoolId);

            if (AllCoursesOfThisSchool.Count() == 0) return Ok("No Courses Found.");

            return Ok(AllCoursesOfThisSchool);



        }
    }
}
