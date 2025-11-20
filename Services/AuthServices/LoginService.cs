using LmsApp2.Api.DTOs;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.Utilities;
using LmsApp2.Api.UtilitiesInterfaces;

namespace LmsApp2.Api.Services.AuthServices
{
    public class LoginService(IEmployeeRepo empRepo,IJwtServices JwtServices) : ILoginService
    {
        public async Task<string> AdminLogin(LoginDto LoginData)
        {
            // we have to first check the credentials of admin if its true then generate a jwt token that we have to
            // save access token in cokkies and refresh token in database and cookies, store session in the database , thats it.
            int EmployeeId = await empRepo.AuthorizeEmployeeAsAdmin(LoginData.Email, LoginData.Password);


            string AccessToken = JwtServices.GenerateAccessTokes(EmployeeId,"Admin");


            return AccessToken;








        }
    }
}
