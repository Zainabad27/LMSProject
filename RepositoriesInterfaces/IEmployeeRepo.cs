using LmsApp2.Api.DTOs;
using LmsApp2.Api.Utilities;

namespace LmsApp2.Api.RepositoriesInterfaces
{
    public interface IEmployeeRepo
    {
        public Task<SendEmployeeToFrontend?> GetEmployeeById(Guid employeeId);
        public Task<Pagination<SendTeachersToFrontend>> GetAllTeachers(int pageNumber, int pageSize);
        public Task<bool> CheckTeacherAndHisCourses(Guid TeacherId,Guid CourseId);
        public Task<Guid> AssignCourse(Guid TeacherId, Guid CourseId);

        public Task<Guid> AddEmployee(EmployeeDto emp, Guid SchoolId,string designation);

        public Task<Guid> MakeEmployeeUserAccount(EmployeeDto emp, Guid EmployeeIdOnEmployeesTable);

        public Task<bool> EmployeeEmailAlreadyExists(string email);

        public Task<Guid> AddEmployeeDocuments(Guid EmpId, string PhotoPath, string CnicBackPath, string CnicFrontPath);

        public Task<(Guid EmployeeAccountId, Guid EmployeeId)> AuthorizeEmployee(string email, string pass,string Designation);

        public Task<Guid> PopulateEmployeeSession(Guid employeeId,string refreshToken);

        public Task<Guid> UpdateEmployeeSession(Guid EmployeeId,string refreshToken);

        public Task<bool> ValidateEmployeeRefreshToken(Guid EmployeeId,string refreshToken);

        public Task<(Guid EmployeeAccountId, Guid EmployeeId)> AuthorizeEmployeeAsTeacher(string email, string pass);

        public Task<(Guid EmployeeAccountId, Guid EmployeeId)> AuthorizeEmployee(string email, string pass);

        public Task<Guid> GetEmployee(Guid EmployeeId);
        public Task SaveChanges();



    }
}
