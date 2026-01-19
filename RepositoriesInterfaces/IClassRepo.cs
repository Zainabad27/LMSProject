using LmsApp2.Api.DTOs;
using LmsApp2.Api.Models;

namespace LmsApp2.Api.RepositoriesInterfaces
{
    public interface IClassRepo
    {
        public Task<List<SendAllClassesToFrontendDto>> GetAllClasses(Guid SchoolId);
        public Task<Guid> AssignCourseToAClass(Guid CourseId, Guid ClassId);
        public Task<List<AssignmentResponse>> GetAllAssignmentsOfClass(Guid? ClassId, Guid CourseId);
        public Task<(bool, String)> CheckClassAndItsCourses(Guid ClassId, Guid CourseId);
        public Task<Guid> AddClass(ClassDto Class, Guid SchoolId);
        public Task<Guid> GetClass(Guid ClassId);
        public Task<Guid> GetClass(Guid SchoolId, string ClassSection, string ClassGrade);
        public Task<Guid> AddCourse(CourseDto CourseData);
        public Task<Guid> GetACourse(Guid ClassId, string CourseName, string boardOrDepartment);
        public Task SaveChanges();
    }
}
