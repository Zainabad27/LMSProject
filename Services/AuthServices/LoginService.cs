using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Identity;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.Utilities;
using LmsApp2.Api.UtilitiesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LmsApp2.Api.Services.AuthServices
{

    public class LoginService(IEmployeeRepo empRepo, IStudentRepo stdRepo, IJwtServices JwtServices, UserManager<AppUser> _userManager) : ILoginService
    {
        public async Task<Guid> AdminLogin(LoginDto LoginData, HttpContext context)
        {


            var (RefreshToken, AccessToken) = await empRepo.AuthorizeEmployeeAndDesignation(LoginData.Email, LoginData.Password, "Admin");

            // we have to first check the credentials of admin if its true then generate a jwt token that we have to
            // save access token in cokkies and refresh token in database and cookies, store session in the database , thats it.

           
            await empRepo.SaveChanges();



            var CookiesOptions = new CookieOptions
            {

                Secure = true,
                HttpOnly = true,



            };

            context.Response.Cookies.Append("AccessToken", AccessToken, CookiesOptions);
            context.Response.Cookies.Append("RefreshToken", RefreshToken, CookiesOptions);

            // Was debugging
            //Logger.LogWarning("Request Cookies: {cookies}",
            //string.Join(", ", context.Request.Cookies.Select(c => $"{c.Key}={c.Value}")));



            return user.UserId_InMainTable;


        }
        public async Task<Guid> TeacherLogin(LoginDto LoginData, HttpContext context)
        {
            // we have to first check the credentials of admin if its true then generate a jwt token that we have to
            // save access token in cokkies and refresh token in database and cookies, store session in the database , thats it.
            var (EmployeeAccountId, EmployeeId) = await empRepo.AuthorizeEmployee(LoginData.Email, LoginData.Password, "Teacher");


            string AccessToken = JwtServices.GenerateAccessToken(EmployeeId, "Teacher", LoginData.Email); // in access token we have put Employee Id in the Token payload not the Account Id.
            string RefreshToken = JwtServices.GenerateRefreshToken();
            // generated the access token now i have to generate refresh token and put the both refresh and access token into the database.


            Guid SessionId = await empRepo.PopulateEmployeeSession(EmployeeAccountId, RefreshToken);


            await empRepo.SaveChanges();

            var CookiesOptions = new CookieOptions
            {

                Secure = true,
                HttpOnly = true,



            };

            context.Response.Cookies.Append("AccessToken", AccessToken, CookiesOptions);
            context.Response.Cookies.Append("RefreshToken", RefreshToken, CookiesOptions);


            return EmployeeId;


        }
        public async Task<Guid> StudentLogin(LoginDto LoginData, HttpContext context)
        {
            // we have to first check the credentials of admin if its true then generate a jwt token that we have to
            // save access token in cokkies and refresh token in database and cookies, store session in the database , thats it.

            var (StdId, StdAccId) = await stdRepo.AuthorizeStudent(LoginData.Email, LoginData.Password);
            string AccessToken = JwtServices.GenerateAccessToken(StdId, "Student", LoginData.Email); // in access token we have put Employee Id in the Token payload not the Account Id.
            string RefreshToken = JwtServices.GenerateRefreshToken();
            // generated the access token now i have to generate refresh token and put the both refresh and access token into the database.


            Guid SessionId = await stdRepo.PopulateStudentSession(StdAccId, RefreshToken);


            await stdRepo.SaveChanges();

            var CookiesOptions = new CookieOptions
            {

                Secure = true,
                HttpOnly = true,



            };

            context.Response.Cookies.Append("AccessToken", AccessToken, CookiesOptions);
            context.Response.Cookies.Append("RefreshToken", RefreshToken, CookiesOptions);


            return StdId;


        }
    }
}
