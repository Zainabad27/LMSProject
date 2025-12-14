using LmsApp2.Api.DTOs;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClassController(IClassService ClassServices) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost("AddClass")]
        public async Task<IActionResult> AddClass([FromBody] ClassDto Class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            Guid ClassId = await ClassServices.AddClass(Class);



            return Ok(new { AddedClassId = ClassId });



        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse([FromBody] CourseDto CourseData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid CourseId = await ClassServices.AddCourseToAClass(CourseData);


            return Ok(new { AddedCourseId = CourseId });



        }


        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> EnrollInAClass([FromBody] EnrollClassDto EnrollmentData)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            Guid ClassId = await ClassServices.EnrollStudent(EnrollmentData);


            await HttpContext.Response.WriteAsync("Student Enrolled Successfuly.");
            return Ok(new { ClassId = ClassId });   






        }




    }
}
