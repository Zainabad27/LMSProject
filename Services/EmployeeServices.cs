using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.Utilities;

namespace LmsApp2.Api.Services
{
    public class EmployeeServices(IEmployeeRepo employeerepo, IEdRepo edRepo, ISchoolRepo schoolrepo, IWebHostEnvironment env) : IEmployeeService
    {
        public async Task<Guid> UploadAssignment(AssignmentUploadDto assignmentData)
        {
            throw new NotImplementedException();

        }

        public async Task<Guid> AssignCourseToTeacher(Guid TeacherId, Guid CourseId)
        {

            // first we have to check if that Teacher and Course Both exists 
            // then in course we have to just put that teacher's id then that course is assigned to that teacher.


            Guid TeacherIdReturned = await employeerepo.GetEmployee(TeacherId);
            if (TeacherId == Guid.Empty)
            {
                throw new CustomException("Teacher Does not Exists in the Database.", 400);
            }

            // while assigning the course from dbmodel we are also checking in a single query that if coure exists or not if not then throwing error.

            Guid CourseIdReturned = await employeerepo.AssignCourse(TeacherIdReturned, CourseId);

            await employeerepo.SaveChanges();

            return CourseIdReturned;


        }
        public async Task<Guid> AddEmployee(EmployeeDto emp, String Designation)
        {
            Guid SchoolId = await schoolrepo.GetSchoolByName(emp.SchoolName);
            if (SchoolId == Guid.Empty)
            {
                throw new Exception("School You are Registering For was not found in the Database.");
            }



            Guid ReturnedEmpId = await employeerepo.AddEmployee(emp, SchoolId, Designation);

            // now we have to make Emplloyees Account too and return it too.
            bool UserEmailAlreadyExists = await employeerepo.EmployeeEmailAlreadyExists(emp.Email);
            if (UserEmailAlreadyExists)
            {

                throw new Exception("This Email is Already in use, Please Enter a different email.");
            }
            Guid EmployeeAccountId = await employeerepo.MakeEmployeeUserAccount(emp, ReturnedEmpId);



            // now we have to upload the necessary documents of Employee and also save it too Server

            var DirectoryPath = Path.Combine(env.WebRootPath, "Documents");


            string PhotoFilePathOnServer = await emp.photo.UploadToServer(DirectoryPath);
            string CnicFrontFilePathOnServer = await emp.photo.UploadToServer(DirectoryPath);
            string CnicBackFilePathOnServer = await emp.photo.UploadToServer(DirectoryPath);

            Guid DocumentId = await employeerepo.AddEmployeeDocuments(ReturnedEmpId, PhotoFilePathOnServer, CnicFrontFilePathOnServer, CnicBackFilePathOnServer);

            await employeerepo.SaveChanges();
            return ReturnedEmpId;




        }
    }
}
