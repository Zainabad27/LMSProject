using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.RepositoriesInterfaces
{
    public interface IEmployeeRepo
    {
        public Task<Guid> AddEmployee(EmployeeDto emp, Guid SchoolId, string SchoolName);

        public Task<Guid> MakeEmployeeUserAccount(EmployeeDto emp, Guid EmployeeIdOnEmployeesTable);


        public Task<bool> EmployeeEmailAlreadyExists(string email);

        public Task<Guid> AddEmployeeDocuments(Guid EmpId, string PhotoPath, string CnicBackPath, string CnicFrontPath);


        public Task<(Guid EmployeeAccountId, Guid EmployeeId)> AuthorizeEmployeeAsAdmin(string email, string pass);



        public Task<Guid> PopulateEmployeeSession(Guid employeeId,string refreshToken);

        public Task<Guid> UpdateEmployeeSession(Guid EmployeeId,string refreshToken);



        public Task<bool> ValidateEmployeeRefreshToken(Guid EmployeeId,string refreshToken);


        public Task<(Guid EmployeeAccountId, Guid EmployeeId)> AuthorizeEmployeeAsTeacher(string email, string pass);


        public Task SaveChanges();



    }
}
