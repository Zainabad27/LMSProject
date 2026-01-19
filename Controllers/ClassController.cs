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
        [HttpGet("GetAllClasses/{SchoolId}")]
        public async Task<IActionResult> GetAllClasses([FromRoute] Guid SchoolId)
        {
            // throw new NotImplementedException();
            var Classes = await ClassServices.GetAllClasses(SchoolId);

            return Ok(Classes);
            
        }



        [Authorize(Roles = "Admin")]
        [HttpPost("AddClass")]
        public async Task<IActionResult> AddClass([FromBody] ClassDto Class)
        {
            Guid ClassId = await ClassServices.AddClass(Class);



            return Ok(new { AddedClassId = ClassId });

        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse([FromBody] CourseDto CourseData)
        {
           

            Guid CourseId = await ClassServices.AddCourse(CourseData);


            return Ok(new { AddedCourseId = CourseId });



        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AssignCourseToClass")]
        public async Task<IActionResult> AssignCourseToClass([FromBody] AssignCourseToClassDto Data)
        {
           await ClassServices.AssignCourseToAClass(Data.ClassId, Data.CourseId);   


            return Ok(new { Message = "Course Assigned to Class Successfully" }); 

        }


        [HttpPost("EnrollStudent")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> EnrollInAClass([FromBody] EnrollClassDto EnrollmentData)
        {

            Guid Classid = await ClassServices.EnrollStudent(EnrollmentData);


            //await HttpContext.Response.WriteAsync("Student Enrolled Successfuly.");
            return Ok(new { ClassId = Classid });   




        }


    }
}
