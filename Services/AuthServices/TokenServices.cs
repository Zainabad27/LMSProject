using LmsApp2.Api.DTOs;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.UtilitiesInterfaces;

namespace LmsApp2.Api.Services.AuthServices
{
    public class TokenServices(IEmployeeRepo empRepo, IJwtServices JwtServices) : ITokenServices
    {
        public async Task<Guid> RefreshAccesToken(RefreshAccessTokenDto RefreshTokenData, HttpContext context)
        {
            var (AccountId, EmployeeId) = await empRepo.AuthorizeEmployeeAsAdmin(RefreshTokenData.Email, RefreshTokenData.Password);

            bool ValidToken = await empRepo.ValidateEmployeeRefreshToken(EmployeeId, RefreshTokenData.RefreshToken);


            if (ValidToken)
            {
                string NewAccessToken = JwtServices.GenerateAccessToken(EmployeeId, "Admin", RefreshTokenData.Email);
                string NewRefreshToken = JwtServices.GenerateRefreshToken();

                await empRepo.UpdateEmployeeSession(EmployeeId, NewRefreshToken);
                await empRepo.SaveChanges();

                context.Response.Cookies.Append("AccessToken", NewAccessToken, new CookieOptions
                {

                    Secure = true,
                    HttpOnly = true,



                });
                context.Response.Cookies.Append("RefreshToken", NewRefreshToken, new CookieOptions
                {

                    Secure = true,
                    HttpOnly = true,


                });


                return EmployeeId;


            }


            throw new Exception("Invalid Refresh Token");










        }
    }
}
