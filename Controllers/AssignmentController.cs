using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LmsApp2.Api.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class AssignmentController(IEmployeeService employeeService) : ControllerBase
    {
        
    
        // swagger issue persists, eating all the memory of our pc
        [HttpPost("UploadAssignment")]
        [Consumes("multipart/form-data")]

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UploadAssigment([FromForm] AssignmentDto AssignmentData)
        {
            var UserClaims = User;
            var TeacherId=UserClaims.FindFirstValue("Id");

            if (TeacherId==null) {
                throw new CustomException("Unauthorized Access.",403);
            
            }

            Guid TId=Guid.Parse(TeacherId);

            Guid AssignmentId= await employeeService.UploadAssignment(AssignmentData,TId);

            return Ok(new { UploadedAssigmentId = AssignmentId });
         

        }
    }
}
