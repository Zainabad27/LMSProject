using LmsApp2.Api.DTOs;
using LmsApp2.Api.Utilities;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<SendCoursesToFrontendDto>> GetAllCourses(Guid TeacherId);
        public Task<ICollection<SendAllSubmissionsToFrontend>> GetAssignmentSubmissions(Guid AssignmentId, Guid TeacherId);
        public Task<bool> MarkAssignment(MarkAssignmentDto MarkingData, Guid TeacherId);
        public Task<SendEmployeeToFrontend> GetEmployeeById(Guid EmployeeId);
        public Task<Pagination<SendTeachersToFrontend>> GetAllTeachers(int PageNumber, int PageSize);
        public Task<ICollection<SendteacherAssignmentsToFrontend>> GetAssignmentsOfTeacher(Guid TeacherId, Guid CourseId);
        public Task<Guid> AssignCourseToTeacher(Guid TeacherId, Guid CourseId);
        public Task<Guid> UploadAssignment(AssignmentDto assignmentData, Guid TeacherId);
    }
}
