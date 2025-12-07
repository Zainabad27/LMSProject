using LmsApp2.Api.DTOs;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.Utilities;

namespace LmsApp2.Api.Services
{
    public class StudentService(IStudentRepo stdRepo, ISchoolRepo schRepo, IWebHostEnvironment env) : IStudentService
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




            var DirectoryPath = Path.Combine(env.WebRootPath, "Documents");


            string PhotoFilePathOnServer = await std.Photo.UploadToServer(DirectoryPath);
            string CnicFrontFilePathOnServer = await std.Cnic_Front.UploadToServer(DirectoryPath);
            string CnicBackFilePathOnServer = await std.Cnic_Back.UploadToServer(DirectoryPath);


            Guid DocId = await stdRepo.AddStudentDocuments(StudentId, PhotoFilePathOnServer, CnicBackFilePathOnServer, CnicFrontFilePathOnServer);

            await stdRepo.SaveChanges();  

            return StudentId;

        }
    }
}
