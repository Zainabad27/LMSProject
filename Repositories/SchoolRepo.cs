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



        public async Task Rollback(AppUser _user)
        {
            bool UserExists = (await _userManager.FindByIdAsync(_user.Id)) != null;
            if (UserExists)
            {
                var result = await _userManager.DeleteAsync(_user);
                if (!result.Succeeded)
                {
                    throw new CustomException("Problem Occured While Rolling back the orperation.", 500);
                }

            }

        }
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

        public async Task<AppUser?> SeedInitialData(Guid SchoolId)
        {

            var DefaultAdminAccount = await SeedAdmin.SeedData(dbcontext, _userManager, SchoolId);
            return DefaultAdminAccount;

        }
    }
}
