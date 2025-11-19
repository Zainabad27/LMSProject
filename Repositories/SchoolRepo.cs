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
        public async Task<int> AddSchool(SchoolDto sch)
        {
            

            var res=await dbcontext.Schools.AddAsync(sch.To_DbModel());
            await dbcontext.SaveChangesAsync();

            School Schoolsaved=res.Entity;

            return Schoolsaved.Schoolid;



        }

        public async Task<int> GetSchoolByName(string name)
        {


            return await dbcontext.Schools.Where(sch => sch.Schoolname == name).Select(sch => sch.Schoolid).FirstOrDefaultAsync();
           



        }
    }
}
