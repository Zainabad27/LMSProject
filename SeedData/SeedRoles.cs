using LmsApp2.Api.Identity;
using Microsoft.AspNetCore.Identity;

namespace LmsApp2.Api.SeedData
{


    internal class SeedRoles()
    {


        public static async Task SeedData(RoleManager<IdentityRole> _roleManager)
        {
                        
            List<string> Roles = ["Admin", "Teacher", "Student"];


            foreach (string Role in Roles)
            {
                bool RoleExists = await _roleManager.RoleExistsAsync(Role);
                if (!RoleExists)
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(Role));


                    if (result.Succeeded)
                    {
                        Console.WriteLine($"{Role} Added In the Database.");
                    }
                    else
                    {
                        Console.WriteLine($"Error creating role '{Role}': {string.Join(", ", result.Errors.Select(e => e.Description))}");

                    }

                }
                else
                {
                    Console.WriteLine($"{Role} Already Exists in the Database.");
                }

            }


        }






    }

}