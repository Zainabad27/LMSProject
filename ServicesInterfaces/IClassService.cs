using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface IClassService
    {

        public Task<Guid> EnrollStudent(EnrollClassDto EnrollmentData);

        public Task<Guid> AddClass(ClassDto Class);

        public Task<List<SendAllClassesToFrontendDto>> GetAllClasses(Guid SchoolId);

        public Task<Guid> AssignCourseToAClass(Guid ClassId, Guid CourseId);
        
        public Task<Guid> AddCourse(CourseDto courseData);
    }
}
