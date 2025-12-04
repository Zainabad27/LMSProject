using LmsApp2.Api.DTOs;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;

namespace LmsApp2.Api.Services
{
    public class SchoolService(ISchoolRepo schoolrepo,IEmployeeRepo empRepo): ISchoolService
    {

        public async Task<Guid> AddSchool(SchoolDto SchoolData)
        {
            // when we add an school we make a default account of an Admin.


            Guid SchoolId=await schoolrepo.AddSchool(SchoolData);


            // making a default Admin Account

            EmployeeDto DefalutAdminAccount = new EmployeeDto() {
                SchoolName=SchoolData.SchoolName,
                EmployeeName="Admin",
                EmployeeDesignation="Admin",
                Email="Admin@gmail.com",
                Password="Admin@123",
                Address="No Address",
                Contact="No Contact",

                // files fields are dummy.

                photo = new FormFile(new MemoryStream(), 0, 0, "photo", "photo.jpg"),
                Cnic_Front = new FormFile(new MemoryStream(), 0, 0, "cnicFront", "cnicFront.jpg"),
                Cnic_Back = new FormFile(new MemoryStream(), 0, 0, "cnicBack", "cnicBack.jpg")
            


        };

            await empRepo.AddEmployee(DefalutAdminAccount,SchoolId);






           return SchoolId;


        }
      
    }
}
