using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface IEmployeeService
    {
        public Task<Guid> AddEmployee(EmployeeDto emp,string designation);


        public Task<List<(Guid AssignmentId,string CourseName)>> GetAssignmentsOfTeacher(Guid TeacherId, Guid CourseId);


        public Task<Guid> AssignCourseToTeacher(Guid TeacherId,Guid CourseId);


        public Task<Guid> UploadAssignment(AssignmentDto assignmentData,Guid TeacherId);
    }
}
