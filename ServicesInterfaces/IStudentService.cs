using LmsApp2.Api.DTOs;
using LmsApp2.Api.Models;
using LmsApp2.Api.Utilities;
using LMSProject.DTOs;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface IStudentService
    {
        public Task<Pagination<SendStudentsToFrontendDto>> GetAllStudentsOfClass(Guid ClassId,int PageNumber,int PageSize);
        public Task<List<SendCoursesToFrontendDto>> GetStudentCourses(Guid StdId);
        public Task<Guid> SubmitAssignment(AssignmentSubmissionDto Submission, Guid StudentId);

        public Task<Guid> AddStudent(StudentDto std);


        public Task<List<AssignmentResponse>> GetAllAssignments(Guid StdId, Guid CourseId);


        // public Task<List<(Guid, String)>> GetStudentCourses(Guid StdId);

        public Task<byte[]> DownloadAssignment(Guid AssignmentId, Guid StdId);



    }
}
