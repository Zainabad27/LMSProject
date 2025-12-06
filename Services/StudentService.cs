using LmsApp2.Api.DTOs;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;

namespace LmsApp2.Api.Services
{
    public class StudentService(IStudentRepo stdRepo) : IStudentService
    {
        public async  Task<Guid> AddStudent(StudentDto std)
        {

            


            return Guid.Empty;
            
        }
    }
}
