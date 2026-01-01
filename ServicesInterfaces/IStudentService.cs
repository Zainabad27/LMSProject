using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface IStudentService
    {

        public Task<Guid> AddStudent(StudentDto std);


        public Task<List<IFormFile>> GetAllAssignments(Guid StdId,String CourseName);


        public Task<List<(Guid, String)>> GetStudentCourse(Guid StdId);



    }
}
