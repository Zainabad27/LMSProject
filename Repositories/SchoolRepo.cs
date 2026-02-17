using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Identity;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LmsApp2.Api.Repositories
{
    public class SchoolRepo(LmsDatabaseContext dbcontext, UserManager<AppUser> _userManager) : ISchoolRepo
    {
  
        public async Task<Guid> AddSchool(SchoolDto sch)
        {



            using var transaction = await dbcontext.Database.BeginTransactionAsync();



            var res = await dbcontext.Schools.AddAsync(sch.To_DbModel());



            // seeding the initial admin when school is beind made 


             await SeedAdmin.SeedData(dbcontext, _userManager, res.Entity.Schoolid);

            await dbcontext.SaveChangesAsync();
            await transaction.CommitAsync();





            return res.Entity.Schoolid;



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
