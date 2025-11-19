using LmsApp2.Api.DTOs;
using LmsApp2.Api.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Controllers
{
    [Route("api/v1/[controller]s/addEmployee")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeServices) : ControllerBase
    {
        [Consumes("multipart/form-data")]
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromForm] EmployeeDto emp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            int addedEmployeeId=await employeeServices.AddEmployee(emp);


            return Ok(new { AddedEmployeeId = addedEmployeeId });

        }
    }
}
