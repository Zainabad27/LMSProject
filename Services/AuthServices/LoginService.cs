using LmsApp2.Api.DTOs;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;

namespace LmsApp2.Api.Services.AuthServices
{
    public class LoginService(IEmployeeRepo empRepo) : IAuthService
    {
        public async Task<string> AdminLogin(LoginDto LoginData)
        {
            // we have to first check the credentials of admin if its true then generate a jwt token that we have to
            // save access token in cokkies and refresh token in database and cookies, store session in the database , thats it.
            int EmployeId = await empRepo.AuthorizeEmployeeAsAdmin(LoginData.Email, LoginData.Password);

            





        }
    }
}
