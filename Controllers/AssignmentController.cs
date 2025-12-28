using LmsApp2.Api.DTOs;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController(IEmployeeService employeeService) : ControllerBase
    {
        [HttpPost("UploadAssignment")]
        //[Consumes("multipart/form-data")]

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UploadAssigment([FromForm] AssignmentUploadDto AssignmentData)
        {

           Guid AssignmentId= await employeeService.UploadAssignment(AssignmentData);

            return Ok(new { UploadedAssigmentId = AssignmentId });

        }
    }
}
