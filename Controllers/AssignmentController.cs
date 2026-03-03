using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.ServicesInterfaces;
using LMSProject.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LmsApp2.Api.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class AssignmentController(IEmployeeService employeeService, IStudentService stdService) : ControllerBase
    {
        [HttpGet("GetSubmissions/{AssignmentId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetAssignmentSubmissions([FromRoute] Guid AssignmentId)
        {
            var userClaims = User;
            var TeacherId = userClaims.FindFirstValue("MainTableId") ?? throw new CustomException("Unauthorized Access.", 403);
            Guid TId = Guid.Parse(TeacherId);


            IEnumerable<SendAllSubmissionsToFrontend> AllSubmissions = await employeeService.GetAssignmentSubmissions(AssignmentId, TId);

            return Ok(AllSubmissions);

        }



        [HttpGet("GetAssignments/{CourseId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetAssigments([FromRoute] Guid CourseId)
        {
            var userClaims = User;
            var TeacherId = userClaims.FindFirstValue("MainTableId") ?? throw new CustomException("Unauthorized Access.", 403);
            Guid TId = Guid.Parse(TeacherId);


            IEnumerable<SendteacherAssignmentsToFrontend> AllAssignments = await employeeService.GetAssignmentsOfTeacher(TId, CourseId);


            return Ok(AllAssignments);

        }


        [HttpPost("MarkAssignment")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> MarkAssignment([FromBody] MarkAssignmentDto MarkingData)
        {
            var userClaims = User;
            var TId = userClaims.FindFirstValue("MainTableId") ?? throw new CustomException("Unauthorized Access.", 403);
            Guid TeacherId = Guid.Parse(TId);

          bool checkedsuccessfully = await employeeService.MarkAssignment(MarkingData, TeacherId);
            if (!checkedsuccessfully)
            {
                throw new CustomException("Failed to Mark Assignment.", 500);
            }
            return Ok(new { Message = "Assignment Marked Successfully" });

        }


        [Consumes("multipart/form-data")]
        [HttpPost("AssignmentSubmission")]
        [Authorize(Roles = "Student")]

        public async Task<IActionResult> AssignmentSubmission([FromForm] AssignmentSubmissionDto submission)
        {

            var UserClaims = User;
            var StudentId = UserClaims.FindFirstValue("MainTableId") ?? throw new CustomException("Unauthorized Access.", 403);
            Guid SId = Guid.Parse(StudentId);


            Guid SubmittedAssId = await stdService.SubmitAssignment(submission, SId);


            return Ok(new { SubmittedAssignmentId = SubmittedAssId });


        }

        [HttpPost("UploadAssignment")]
        [Consumes("multipart/form-data")]

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UploadAssigment([FromForm] AssignmentDto AssignmentData)
        {
            var UserClaims = User;
            var TeacherId = UserClaims.FindFirstValue("MainTableId") ?? throw new CustomException("Unauthorized Access.", 403);
            Guid TId = Guid.Parse(TeacherId);

            Guid AssignmentId = await employeeService.UploadAssignment(AssignmentData, TId);

            return Ok(new { UploadedAssigmentId = AssignmentId });


        }
    }
}
