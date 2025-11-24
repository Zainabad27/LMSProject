using LmsApp2.Api.DTOs;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;

namespace LmsApp2.Api.Services
{
    public class SchoolService(ISchoolRepo schoolrepo): ISchoolService
    {

        public async Task<int> AddSchool(SchoolDto SchoolData)
        {

            int SchoolId=await schoolrepo.AddSchool(SchoolData);




           return SchoolId;


        }
      
    }
}
