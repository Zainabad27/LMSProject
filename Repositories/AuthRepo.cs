using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Identity;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.UtilitiesInterfaces;
using Microsoft.AspNetCore.Identity;

namespace LmsApp2.Api.Repositories
{
    internal class AuthRepo(UserManager<AppUser> _userManager, LmsDatabaseContext dbcontext, IJwtServices JwtServices) : IAuthRepo
    {
        public async Task<SendLoginDataToFrontend> Login(string email, string pass, string designation)
        {
            using var transaction = await dbcontext.Database.BeginTransactionAsync();

            var user = await _userManager.FindByEmailAsync(email) ?? throw new CustomException("Email not Found", 400);
            bool PasswordCorrect = await _userManager.CheckPasswordAsync(user, pass);

            bool IsDesignationCorrect = await _userManager.IsInRoleAsync(user, designation);


            if (!PasswordCorrect)
            {
                throw new CustomException("Invalid Password Given.", 400);
            }


            if (!IsDesignationCorrect)
            {
                throw new CustomException($"These Credentials are not of {designation}.", 400);
            }

            string AccessToken = JwtServices.GenerateAccessToken(user.UserId_InMainTable, designation, email); // in access token we have put Employee Id in the Token payload not the Account Id.
            string RefreshToken = JwtServices.GenerateRefreshToken();


            user.RefreshToken = RefreshToken;
            user.TokenExpiry = DateTime.UtcNow.AddDays(3);



            await transaction.CommitAsync();





            return new SendLoginDataToFrontend()
            {
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
                UserId_InMainTable = user.UserId_InMainTable
            };


        }

        public async Task SaveChanges()
        {
            await dbcontext.SaveChangesAsync();
        }
    }

}