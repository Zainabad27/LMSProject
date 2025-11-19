using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface ISchoolService
    {
        public Task<int> AddSchool(SchoolDto SchoolData);
    }
}
