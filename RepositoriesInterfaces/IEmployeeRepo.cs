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

        public Task<(Guid EmployeeId, Guid DocumentId)> AddEmployee(EmployeeDto emp, Guid SchoolId, string designation, Dictionary<string, string> Docs);

        public Task<bool> EmployeeEmailAlreadyExists(string email);

       public  Task<Guid> AddEmployeeDocuments(Guid EmpId, Dictionary<string, string> Docs);
    

        // public Task<bool> ValidateEmployeeRefreshToken(Guid EmployeeId,string refreshToken);

       public Task<SendLoginDataToFrontend> AuthorizeEmployeeAndDesignation(string email, string pass, string designation);

        public Task<Guid> GetEmployee(Guid EmployeeId);
        public Task SaveChanges();



    }
}
