using LmsApp2.Api.DTOs;
using LmsApp2.Api.Models;
using LMSProject.DTOs;

namespace LmsApp2.Api.RepositoriesInterfaces
{
    public interface IStudentRepo
    {
         public  Task<List<SendCoursesToFrontendDto>> GetStudentCourses(Guid ClassId);
       public Task<(bool ValidStudent,Guid? ClassId)> ValidStudent(Guid StudentId);
        public Task<Guid> SubmitAssignment(AssignmentSubmissionDto Submission, Guid StudentId, string SubmissionFilePathOnServer);
        public Task<Student?> GetStudent(Guid StudentId);
        public Task<Guid?> GetStudentClass(Guid StdId);
        public Task<(Guid StudentId, Guid AccountId)> AuthorizeStudent(string email, string Password);

        public Task<Guid> AddStudent(StudentDto std, Guid SchoolId);

        public Task<Guid> MakeStudentAccount(StudentDto std, Guid StudentId);

        public Task<Guid> AddStudentDocuments(Guid StdId, string PhotoPath, string CnicBackPath, string CnicFrontPath);

        public Task SaveChanges();

        public Task<Guid> PopulateStudentSession(Guid StdAccId, string RefreshToken);

    }
}
