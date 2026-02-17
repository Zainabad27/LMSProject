using System.Linq.Expressions;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Identity;
using LmsApp2.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LmsApp2.Api.SeedData
{
    internal class SeedAdmin
    {
        public static async Task<AppUser?> SeedData(LmsDatabaseContext dbcontext, UserManager<AppUser> _userManager, Guid SchoolId)
        {
            
            // we will check if the Employee Table is Empty then we will seend the initial admin
            Guid AdminId = Guid.NewGuid();

            bool IsAdminPresent = await dbcontext.Employees.AnyAsync();
            if (!IsAdminPresent)
            {
                Employee DefaultAdmin = new()
                {
                    Schoolid = SchoolId,
                    Employeeid = AdminId,
                    Createdat = DateTime.UtcNow,
                    Isactive = true,
                    Employeedesignation = "Admin",



                };

                await dbcontext.Employees.AddAsync(DefaultAdmin);

            }




            // seed the initial admin Account at the start of the App on if the Database is Empty(App starting for the very first time.)

            // _userManager.FindByIdAsync();
            var DefaultAdminExists = await _userManager.FindByNameAsync("DefaultAdmin"+$"{SchoolId}");
            IsAdminPresent = DefaultAdminExists != null;
            if (!IsAdminPresent)
            {
                AppUser DefaultAdminAccount = new()
                {
                    UserId_InMainTable = AdminId,
                    UserName = "DefaultAdmin"+$"{SchoolId}",
                    Email = $"Admin{SchoolId}@gmail.com",
                };


                var result = await _userManager.CreateAsync(DefaultAdminAccount, "Admin@123");
                bool succ = result.Succeeded;
                if (!succ)
                {
                    // Console.Error

                    Console.WriteLine($"Error: {result.Errors.Select(e => e.Description)}");
                    throw new CustomException("Problem Occured While Seeding the Initial Data into the DB", 500);
                }

                await _userManager.AddToRoleAsync(DefaultAdminAccount, "Admin");


                return DefaultAdminAccount;
            }


            return null;


        }
    }
}