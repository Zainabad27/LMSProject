using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Identity;
using LmsApp2.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LmsApp2.Api.SeedData
{
    internal class SeedAdmin
    {
        public static async Task SeedData(LmsDatabaseContext dbcontext,UserManager<AppUser> _userManager,Guid SchoolId)
        {
            using var transaction=await dbcontext.Database.BeginTransactionAsync();
            // we will check if the Employee Table is Empty then we will seend the initial admin



            // seed the initial admin at the start of the App on if the Database is Empty(App starting for the very first time.)
            bool IsAdminPresent = await dbcontext.Employees.AnyAsync();

            if (!IsAdminPresent)
            {
               Guid AdminId=Guid.NewGuid();


                Employee DefaultAdmin = new()
                {
                    Schoolid=SchoolId,
                    Employeeid=AdminId,
                    Createdat=DateTime.UtcNow,
                    Isactive=true,


                    
                };


                AppUser DefaultAdminAccount = new()
                {
                    UserId_InMainTable=AdminId,
                    UserName="DefaultAdmin",
                    Email="Admin@Gmail.com",
                };


               var result=await  _userManager.CreateAsync(DefaultAdminAccount,"Admin@123");

                if (!result.Succeeded)
                {
                    throw new CustomException("Problem Occured While Seeding the Initial Data into the DB",500);
                }

                await _userManager.AddToRoleAsync(DefaultAdminAccount,"Admin");



               await  dbcontext.Employees.AddAsync(DefaultAdmin);

               await transaction.CommitAsync();



            }


        }
    }
}