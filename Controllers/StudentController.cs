using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LmsApp2.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentController(IStudentService StdService) : ControllerBase
    {
       
        [HttpGet("GetAssignments/{CourseId}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetAllAssignments([FromRoute] Guid CourseId)
        {
            var Id = User.FindFirstValue("MainTableId");
            if (Id == null)
            {
                throw new CustomException("Unauthorized Access.");
            }

            Guid StdId = Guid.Parse(Id);

            List<AssignmentResponse> AllAssignments = await StdService.GetAllAssignments(StdId, CourseId);

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
            var Id = User.FindFirstValue("MainTableId");
            if (Id == null)
            {
                throw new CustomException("Unauthorized Access.");
            }

            Guid StdId = Guid.Parse(Id);


            byte[] FileData = await StdService.DownloadAssignment(AssignmentId, StdId);

            return File(FileData, "image/jpg");

        }

        [HttpGet("GetAllStudents/{ClassId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllStudentsOfAClass([FromRoute] Guid ClassId, [FromQuery] int Page = 1, [FromQuery] int PageSize = 10)
        {
            Pagination<SendStudentsToFrontendDto> Students = await StdService.GetAllStudentsOfClass(ClassId, Page, PageSize);

            return Ok(Students);
        }


        [HttpGet("GetAllCourses")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetAllCourses()
        {
            var Id = User.FindFirstValue("MainTableId") ?? throw new CustomException("Unauthorized Access.");
            Guid StdId = Guid.Parse(Id);

            List<SendCoursesToFrontendDto> Courses = await StdService.GetStudentCourses(StdId);

            return Ok(Courses);
        }



    }
}
