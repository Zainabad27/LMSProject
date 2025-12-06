using LmsApp2.Api.DTOs;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;

namespace LmsApp2.Api.Services
{
    public class StudentService(IStudentRepo stdRepo, ISchoolRepo schRepo,IWebHostEnvironment env) : IStudentService
    {
        public async Task<Guid> AddStudent(StudentDto std)
        {
            Guid SchoolId = await schRepo.GetSchoolByName(std.SchoolName);

            if (SchoolId == Guid.Empty)
            {
                throw new Exception("The School Student is trying to register in does not Exists.");
            }



            Guid StudentId = await stdRepo.AddStudent(std, SchoolId);


            if (StudentId == Guid.Empty)
            {
                throw new Exception("Some error Occured In the Database while Saving the Student Data.");

            }


            Guid AccountId = await stdRepo.MakeStudentAccount(std, StudentId);






            // first I have to populate students main table then make the student account and upload the images to server




            return Guid.Empty;

        }
    }
}
