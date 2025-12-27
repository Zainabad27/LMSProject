using LmsApp2.Api.DTOs;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [Route("api/v1/[controller]s/addSchool")]
    [ApiController]
    public class SchoolController(ISchoolService SchoolServices) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddSchool([FromBody]SchoolDto School) {


            Guid AddedSchoolId=await SchoolServices.AddSchool(School);

            return Created("Somewhere",new { SchoolId = AddedSchoolId });
        }
    }
}
