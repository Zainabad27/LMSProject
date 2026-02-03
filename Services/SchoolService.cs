using LmsApp2.Api.DTOs;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.SeedData;
using LmsApp2.Api.ServicesInterfaces;

namespace LmsApp2.Api.Services
{
    public class SchoolService(ISchoolRepo schoolrepo, IEmployeeRepo empRepo) : ISchoolService
    {

        public async Task<Guid> AddSchool(SchoolDto SchoolData)
        {
            // when we add an school we make a default account of an Admin.


            Guid SchoolId = await schoolrepo.AddSchool(SchoolData);


            // making a default Admin Account

            await schoolrepo.SeedInitialData(SchoolId);

           await schoolrepo.SaveChanges();
            return SchoolId;


        }

    }
}
