using LmsApp2.Api.DTOs;
using LmsApp2.Api.Services;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [Route("api/v1/[controller]s/addSchool")]
    [ApiController]
    public class SchoolController(ISchoolService SchoolServices) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddSchool([FromBody]SchoolDto School) {

            if (!ModelState.IsValid) {

                return BadRequest(ModelState);


            }

            int AddedSchoolId=await SchoolServices.AddSchool(School);





            return Created("Somewhere",new { SchoolId = AddedSchoolId });
        }
    }
}
