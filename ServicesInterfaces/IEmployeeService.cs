using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface IEmployeeService
    {
        public Task<int> AddEmployee(EmployeeDto emp);
    }
}
