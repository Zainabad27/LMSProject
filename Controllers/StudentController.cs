using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LmsApp2.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentController(IStudentService StdService) : ControllerBase
    {
        [HttpPost("RegisterStudent")]
        [Consumes("multipart/form-data")]
        [AllowAnonymous]
        public async Task<IActionResult> AddStudent([FromForm] StudentDto stdData)
        {



            Guid StudentId = await StdService.AddStudent(stdData);



            return Ok(new { AddedStudentId = StudentId });




        }
        [HttpGet("GetAssignments/{CourseId}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetAllAssignments([FromRoute] Guid CourseId)
        {
            var Id = User.FindFirstValue("Id");
            if (Id == null)
            {
                throw new CustomException("Unauthorized Access.");
            }

            Guid StdId = Guid.Parse(Id);

            List<AssignmentResponse> AllAssignments = await StdService.GetAllAssignments(StdId,CourseId);

            //List<File> Files = new List<File>();

            if (AllAssignments.Count == 0)
            {
                return Ok("No Assignments Due Currently.");
            }

            return Ok(AllAssignments);

        }
        [HttpGet("DownloadAssignment/{AssignmentId}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> DownloadAssignment([FromRoute] Guid AssignmentId)
        {
            var Id = User.FindFirstValue("Id");
            if (Id == null)
            {
                throw new CustomException("Unauthorized Access.");
            }

            Guid StdId = Guid.Parse(Id);


            byte[] FileData = await StdService.DownloadAssignment(AssignmentId, StdId);

            return File(FileData, "image/jpg");

        }

        [HttpGet("GetAllCourses")]
        [Authorize(Roles = "Student")]

        public async Task<IActionResult> GetAllCourses()
        {
            var Id = User.FindFirstValue("Id");
            if (Id == null)
            {
                throw new CustomException("Unauthorized Access.");
            }

            Guid StdId = Guid.Parse(Id);

            List<SendCoursesToFrontendDto> Courses = await StdService.GetStudentCourses(StdId);

            return Ok(Courses);
        }



    }
}
