using LmsApp2.Api.DTOs;
using LmsApp2.Api.Identity;
using LmsApp2.Api.Models;

namespace LmsApp2.Api.RepositoriesInterfaces
{
    public interface ISchoolRepo
    {
        public Task<Guid> AddSchool(SchoolDto sch);

        public Task<Guid> GetSchoolByName(string name);


        public Task SaveChanges();
    }
}
