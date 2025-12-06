using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface IStudentService
    {

        public Task<Guid> AddStudent(StudentDto std);



    }
}
