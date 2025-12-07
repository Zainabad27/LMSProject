using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.RepositoriesInterfaces
{
    public interface IStudentRepo
    {

        public Task<(Guid StudentId, Guid AccountId)> AuthorizeStudent(string email, string Password);

        public Task<Guid> AddStudent(StudentDto std, Guid SchoolId);

        public Task<Guid> MakeStudentAccount(StudentDto std, Guid StudentId);

        public Task<Guid> AddStudentDocuments(Guid StdId, string PhotoPath, string CnicBackPath, string CnicFrontPath);

        public Task SaveChanges();

    }
}
