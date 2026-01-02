using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.Utilities;

namespace LmsApp2.Api.Services
{
    public class StudentService(IStudentRepo stdRepo, ISchoolRepo schRepo,IClassRepo clsRepo ,IWebHostEnvironment env) : IStudentService
    {
        public async Task<Guid> AddStudent(StudentDto std)
        {
            Guid SchoolId = await schRepo.GetSchoolByName(std.SchoolName);

            if (SchoolId == Guid.Empty)
            {
                throw new CustomException("The School Student is trying to register in does not Exists.",401);
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

        public async Task<List<IFormFile>> GetAllAssignments(Guid StdId,Guid CourseId)
        {
            // first we have to check the students is real student 
            // then we have to check his class and in that class we will fetch the assignments for that particular course.
            // assignments are saved on the server in the form of jpeg or other file we have to fetch it from there DB only saves the Path of it on the server in the Database.

            Guid StdClass=await stdRepo.GetStudentClass(StdId);


            //clsRepo.GetAllAssignmentsOfClass();




            throw new NotImplementedException();    

        }

        public Task<List<(Guid, string)>> GetStudentCourse(Guid StdId)
        {
            throw new NotImplementedException();
        }
    }
}
