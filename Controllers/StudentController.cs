using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LmsApp2.Api.Controllers
{
    [Route("api/[controller]")]
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
        [HttpPost("GetAssignments")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetAllAssignments([FromBody] String CourseName)
        {
            var Id = User.FindFirstValue("Id");
            if (Id == null)
            {
                throw new CustomException("Unauthorized Access.");
            }

            Guid StdId = Guid.Parse(Id);

            await StdService.GetAllAssignments(StdId, CourseName);


            throw new NotImplementedException();

        }
    }
}
