using LmsApp2.Api.DTOs;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LmsApp2.Api.Repositories
{
    public class SchoolRepo(LmsDatabaseContext dbcontext) : ISchoolRepo
    {
        public async Task<Guid> AddSchool(SchoolDto sch)
        {


            var res = await dbcontext.Schools.AddAsync(sch.To_DbModel());


            School Schoolsaved = res.Entity;

            return Schoolsaved.Schoolid;



        }

        public async Task<Guid> GetSchoolByName(string name)
        {

            return await dbcontext.Schools.Where(sch => sch.Schoolname == name).Select(sch => sch.Schoolid).FirstOrDefaultAsync();


        }

        public async Task SaveChanges()
        {
            await dbcontext.SaveChangesAsync();
        }
    }
}
