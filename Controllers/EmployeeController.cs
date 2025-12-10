using LmsApp2.Api.DTOs;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeServices) : ControllerBase
    {
        [Authorize(Roles="Admin")]
        [Consumes("multipart/form-data")]
        [HttpPost("AddAdmin")]
        //[HttpPost]
        public async Task<IActionResult> AddAdmin([FromForm] EmployeeDto emp)
        {
            var u= User;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            Guid addedEmployeeId=await employeeServices.AddEmployee(emp,"Admin");

            var context = HttpContext;


            Console.WriteLine(context);
            return Ok(new { AddedEmployeeId = addedEmployeeId });

        }
        [Authorize(Roles ="Admin")]
        [HttpPost("AddTeacher")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddTeacher([FromForm] EmployeeDto emp)
        {
         
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            Guid addedEmployeeId=await employeeServices.AddEmployee(emp,"Teacher");

            var context = HttpContext;


            Console.WriteLine(context);
            return Ok(new { AddedEmployeeId = addedEmployeeId });

        }
    }
}
