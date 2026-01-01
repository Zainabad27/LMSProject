using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface IEmployeeService
    {
        public Task<Guid> AddEmployee(EmployeeDto emp,string designation);


        public Task<Guid> AssignCourseToTeacher(Guid TeacherId,Guid CourseId);


        public Task<Guid> UploadAssignment(AssignmentDto assignmentData);
    }
}
