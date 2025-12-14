using LmsApp2.Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        [HttpPost("UploadAssignment/{TeacherId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UploadAssigment(Guid TeacherId, [FromBody] AssignmentUploadDto AssignmentData) {
            if (!ModelState.IsValid) {
            return BadRequest(ModelState);
            }


            throw new NotImplementedException();    
        
            
        
        }
    }
}
