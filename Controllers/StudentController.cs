using LmsApp2.Api.DTOs;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(IStudentService StdService) : ControllerBase
    {
        [HttpPost("RegisterStudent")]
        [Consumes("multipart/form-data")]
        [AllowAnonymous]
        public async Task<IActionResult> AddStudent([FromForm] StudentDto stdData) {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            await StdService.AddStudent(stdData);

            throw new NotImplementedException();    
        
        
        }
    }
}
