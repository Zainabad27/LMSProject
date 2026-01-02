using LmsApp2.Api.DTOs;
using LmsApp2.Api.Models;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface IStudentService
    {

        public Task<Guid> AddStudent(StudentDto std);


        public Task<List<IFormFile>> GetAllAssignments(Guid StdId,Guid CourseId);


        public Task<List<(Guid, String)>> GetStudentCourse(Guid StdId);



    }
}
