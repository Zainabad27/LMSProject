using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.RepositoriesInterfaces
{
    public interface IEmployeeRepo
    {
        public Task<int> AddEmployee(EmployeeDto emp, int SchoolId, string SchoolName);

        public Task<int> MakeEmployeeUserAccount(EmployeeDto emp,int EmployeeIdOnEmployeesTable);


        public Task<bool> EmployeeEmailAlreadyExists(string email);

        public Task<int> AddEmployeeDocuments(int EmpId, string PhotoPath, string CnicBackPath, string CnicFrontPath);



    }
}
