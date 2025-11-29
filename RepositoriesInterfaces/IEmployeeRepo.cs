using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.RepositoriesInterfaces
{
    public interface IEmployeeRepo
    {
        public Task<int> AddEmployee(EmployeeDto emp, int SchoolId, string SchoolName);

        public Task<int> MakeEmployeeUserAccount(EmployeeDto emp,int EmployeeIdOnEmployeesTable);


        public Task<bool> EmployeeEmailAlreadyExists(string email);

        public Task<int> AddEmployeeDocuments(int EmpId, string PhotoPath, string CnicBackPath, string CnicFrontPath);


        public Task<(int EmployeeAccountId, int EmployeeId)> AuthorizeEmployeeAsAdmin(string email, string pass);



        public Task<int> PopulateEmployeeSession(int employeeId,string refreshToken);

        public Task<int> UpdateEmployeeSession(int EmployeeId,string refreshToken);



        public Task<bool> ValidateEmployeeRefreshToken(int EmployeeId,string refreshToken);


        public Task<(int EmployeeAccountId, int EmployeeId)> AuthorizeEmployeeAsTeacher(string email, string pass);


        public Task SaveChanges();



    }
}
