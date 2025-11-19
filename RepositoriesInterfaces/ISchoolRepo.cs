using LmsApp2.Api.DTOs;
using LmsApp2.Api.Models;

namespace LmsApp2.Api.RepositoriesInterfaces
{
    public interface ISchoolRepo
    {
        public Task<int> AddSchool(SchoolDto sch);

        public Task<int> GetSchoolByName(string name);
    }
}
