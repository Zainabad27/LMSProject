using LmsApp2.Api.Repositories;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.UtilitiesInterfaces;
using System.Security.Claims;

namespace LmsApp2.Api.Middlewares
{
    public class IsAdmin(RequestDelegate next, IJwtServices JwtServices, IEmployeeRepo emprepo)
    {

        public async Task InvokeAsync(HttpContext context)
        {
            var accessToken = context.Request.Cookies["AccessToken"];
            var refreshToken = context.Request.Cookies["RefreshToken"];


            if (String.IsNullOrEmpty(accessToken) || String.IsNullOrEmpty(refreshToken))
            {
                throw new InvalidOperationException("UnAuthorized Request");
            }

            var (principal, Validatetoken) = JwtServices.VerifyJwtToken(accessToken);
            var role = principal.FindFirst("Role")?.Value;
            var Email = principal.FindFirst("Email")?.Value;
            var id = principal.FindFirst("Id")?.Value;

            if(id==null||!(int.TryParse(id,out int Employeeid)))
            {
                throw new Exception("No Employee Id was given in AccessToken");

            }

            if (role != "Admin")
            {
                    throw new Exception("Unauthorized Access,User Is not An admin");
            }

            if (Email == null)
            {
                throw new Exception("No Email inside ");
            }

        



            DateTime ExpiryDate = JwtServices.JwtTokenExpiresAt(accessToken);


            if (ExpiryDate > DateTime.UtcNow)
            {

                context.User = principal;

                await next(context);

            }



          int IsRefreshTokenValid= await emprepo.ValidateEmployeeRefreshToken(Employeeid, refreshToken);

























        }



    }
}
