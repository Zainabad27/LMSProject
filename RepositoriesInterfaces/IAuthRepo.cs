using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.RepositoriesInterfaces
{



    public interface IAuthRepo
    {
        public Task<SendLoginDataToFrontend> Login(string email, string pass, string designation);


        public Task<(Guid EmployeeId, Guid DocId)> RegisterEmployee(EmployeeDto Emp, Guid SchoolId, string designation, Dictionary<string, string> Docs);

        public Task<(Guid StudentId, Guid DocId)> RegisterStudent(StudentDto std, Guid SchoolId, Dictionary<string, string> docs);

        public Task Logout(Guid Id);  


        public Task SaveChanges();
    }

}