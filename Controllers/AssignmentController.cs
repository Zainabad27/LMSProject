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
        [HttpGet("GetAssignments/{CourseId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetAssigments([FromRoute] Guid CourseId)
        {
            var userClaims = User;
            var TeacherId = userClaims.FindFirstValue("Id");
             
             if (TeacherId == null)
             {
                 throw new CustomException("Unauthorized Access.", 403);

             }


             Guid TId = Guid.Parse(TeacherId);


            List<(Guid,string)> AllAssignments= await employeeService.GetAssignmentsOfTeacher(TId, CourseId);




            return Ok(AllAssignments );

        }


        [HttpPost("AssignmentSubmission")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "Student")]

        public async Task<IActionResult> AssignmentSubmission([FromBody] AssignmentSubmissionDto submission)
        {
            var UserClaims = User;
            var StudentId = UserClaims.FindFirstValue("Id");

            if (StudentId == null)
            {
                throw new CustomException("Unauthorized Access.", 403);

            }

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
            var TeacherId = UserClaims.FindFirstValue("Id");

            if (TeacherId == null)
            {
                throw new CustomException("Unauthorized Access.", 403);

            }

            Guid TId = Guid.Parse(TeacherId);

            Guid AssignmentId = await employeeService.UploadAssignment(AssignmentData, TId);

            return Ok(new { UploadedAssigmentId = AssignmentId });


        }
    }
}
