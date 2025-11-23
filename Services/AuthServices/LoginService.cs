using LmsApp2.Api.DTOs;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.Utilities;
using LmsApp2.Api.UtilitiesInterfaces;

namespace LmsApp2.Api.Services.AuthServices
{
    public class LoginService(IEmployeeRepo empRepo, IJwtServices JwtServices) : ILoginService
    {
        public async Task<int> AdminLogin(LoginDto LoginData, HttpContext context)
        {
            // we have to first check the credentials of admin if its true then generate a jwt token that we have to
            // save access token in cokkies and refresh token in database and cookies, store session in the database , thats it.
            var (EmployeeAccountId,EmployeeId) = await empRepo.AuthorizeEmployeeAsAdmin(LoginData.Email, LoginData.Password);


            string AccessToken = JwtServices.GenerateAccessToken(EmployeeId, "Admin", LoginData.Email); // in access token we have put Employee Id in the Token payload not the Account Id.
            string RefreshToken = JwtServices.GenerateRefreshToken();
            // generated the access token now i have to generate refresh token and put the both refresh and access token into the database.


            int SessionId = await empRepo.PopulateEmployeeSession(EmployeeAccountId, RefreshToken, context);


            context.Response.Cookies.Append("AccessToken", AccessToken, new CookieOptions
            {

                Secure = true,
                HttpOnly = true,



            });
            context.Response.Cookies.Append("RefreshToken", RefreshToken, new CookieOptions
            {

                Secure = true,
                HttpOnly = true,


            });


            return EmployeeId;


        }
    }
}
