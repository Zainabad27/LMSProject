using LmsApp2.Api.DTOs;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.Utilities;
using System.IO;

namespace LmsApp2.Api.Services
{
    public class EmployeeServices(IEmployeeRepo employeerepo, ISchoolRepo schoolrepo,IWebHostEnvironment env) : IEmployeeService
    {
        public async Task<int> AddEmployee(EmployeeDto emp)
        {
            int SchoolId = await schoolrepo.GetSchoolByName(emp.SchoolName);
            if (SchoolId == 0)
            {
                throw new Exception("School You are Registering For was not found in the Database.");
            }


            int ReturnedEmpId = await employeerepo.AddEmployee(emp, SchoolId, emp.SchoolName);

            // now we have to make Emplloyees Account too and return it too.
            bool UserEmailAlreadyExists = await employeerepo.EmployeeEmailAlreadyExists(emp.Email);
            if (UserEmailAlreadyExists)
            {

                throw new Exception("This Email is Already in use Please Enter a different email.");
            }
            int EmployeeAccountId = await employeerepo.MakeEmployeeUserAccount(emp, ReturnedEmpId);



            // now we have to upload the necessary documents of Employee and also save it too Server

            var DirectoryPath = Path.Combine(env.WebRootPath,"Documents");
            Console.WriteLine($"{DirectoryPath}");

            string PhotoFilePathOnServer=await emp.photo.UploadToServer(DirectoryPath);
            string CnicFrontFilePathOnServer=await emp.photo.UploadToServer(DirectoryPath);
            string CnicBackFilePathOnServer=await emp.photo.UploadToServer(DirectoryPath);

           int DocumentId= await employeerepo.AddEmployeeDocuments(ReturnedEmpId, PhotoFilePathOnServer, CnicFrontFilePathOnServer,CnicBackFilePathOnServer);

            
                return ReturnedEmpId;




        }
    }
}
