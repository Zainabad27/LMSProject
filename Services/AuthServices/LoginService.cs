using LmsApp2.Api.DTOs;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.UtilitiesInterfaces;


namespace LmsApp2.Api.Services.AuthServices
{

    public class LoginService(IEmployeeRepo empRepo, IStudentRepo stdRepo, IJwtServices JwtServices) : ILoginService
    {
        public async Task<Guid> AdminLogin(LoginDto LoginData, HttpContext context)
        {

            // we have to first check the credentials of admin if its true then generate a jwt token that we have to

            SendLoginDataToFrontend data = await empRepo.AuthorizeEmployeeAndDesignation(LoginData.Email, LoginData.Password, "Admin");

            // save access token in cokkies and refresh token in database and cookies, store session in the database , thats it.


            await empRepo.SaveChanges();



            var CookiesOptions = new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
            };

            context.Response.Cookies.Append("AccessToken", data.AccessToken, CookiesOptions);
            context.Response.Cookies.Append("RefreshToken", data.RefreshToken, CookiesOptions);


            return data.UserId_InMainTable;


        }
        public async Task<Guid> TeacherLogin(LoginDto LoginData, HttpContext context)
        {
            // we have to first check the credentials of admin if its true then generate a jwt token that we have to
            // save access token in cokkies and refresh token in database and cookies, store session in the database , thats it.


            SendLoginDataToFrontend data = await empRepo.AuthorizeEmployeeAndDesignation(LoginData.Email, LoginData.Password, "Teacher");




            // custom auth code :  
            // var (EmployeeAccountId, EmployeeId) = await empRepo.AuthorizeEmployee(LoginData.Email, LoginData.Password, "Teacher");
            // string AccessToken = JwtServices.GenerateAccessToken(EmployeeId, "Teacher", LoginData.Email); // in access token we have put Employee Id in the Token payload not the Account Id.
            // string RefreshToken = JwtServices.GenerateRefreshToken();
            // generated the access token now i have to generate refresh token and put the both refresh and access token into the database.


            // Guid SessionId = await empRepo.PopulateEmployeeSession(EmployeeAccountId, RefreshToken);


            await empRepo.SaveChanges();

            var CookiesOptions = new CookieOptions
            {

                Secure = true,
                HttpOnly = true,



            };

            context.Response.Cookies.Append("AccessToken", data.AccessToken, CookiesOptions);
            context.Response.Cookies.Append("RefreshToken", data.RefreshToken, CookiesOptions);


            return data.UserId_InMainTable;

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
