using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface IClassService
    {
        public Task<Guid> AddClass(ClassDto Class);


        public Task<Guid> AddCourseToAClass(CourseDto courseData);
    }
}
