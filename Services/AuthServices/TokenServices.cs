using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Identity;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.UtilitiesInterfaces;
using Microsoft.AspNetCore.Identity;

namespace LmsApp2.Api.Services.AuthServices
{
    public class TokenServices(IJwtServices JwtServices, UserManager<AppUser> _userManager) : ITokenServices
    {
        public async Task<Guid> RefreshAccesToken(RefreshAccessTokenDto RefreshTokenData, HttpContext context,string Designation)
        {


            var user = await _userManager.FindByEmailAsync(RefreshTokenData.Email) ?? throw new CustomException("Invalid Email Given", 400);
            bool IsPassCorrect = await _userManager.CheckPasswordAsync(user, RefreshTokenData.Password);
            if (!IsPassCorrect)
            {
                throw new CustomException("Incorrect Password", 400);

            }
                bool ValidToken=user.RefreshToken == RefreshTokenData.RefreshToken;

            if (!ValidToken)
            {
                throw new CustomException("Invalid Refresh Token", 400);

            }


            if (user.TokenExpiry < DateTime.Now)
            {
                throw new CustomException("Refresh Token Expired.", 400);
            }



            if (ValidToken)
            {
                string NewAccessToken = JwtServices.GenerateAccessToken(user.UserId_InMainTable, Designation, RefreshTokenData.Email);
                string NewRefreshToken = JwtServices.GenerateRefreshToken();



                user.RefreshToken=NewRefreshToken;
                user.TokenExpiry=DateTime.UtcNow.AddDays(3);

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


                return user.UserId_InMainTable;


            }


            throw new Exception("Invalid Refresh Token");










        }
    }
}
