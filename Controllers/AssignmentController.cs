using LmsApp2.Api.DTOs;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController(IEmployeeService employeeService) : ControllerBase
    {
        [HttpPost("UploadAssignment")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UploadAssigment([FromBody] AssignmentUploadDto AssignmentData)
        {
           


            await employeeService.UploadAssignment(AssignmentData);



            return Ok();




        }
    }
}
