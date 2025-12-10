using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.RepositoriesInterfaces
{
    public interface IClassRepo
    {
        public Task<Guid> AddClass(ClassDto Class,Guid SchoolId);


        public Task<Guid> GetClass(Guid SchoolId,string ClassSection,string ClassGrade);

        public Task<Guid> AddCourse(Guid ClassId, CourseDto CourseData);
        public Task SaveChanges();
    }
}
