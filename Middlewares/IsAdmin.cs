using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.UtilitiesInterfaces;

namespace LmsApp2.Api.Middlewares
{
    public class IsAdmin(RequestDelegate next)
    {

        public async Task InvokeAsync(HttpContext context, IJwtServices JwtServices)
        {
            var accessToken = context.Request.Cookies["AccessToken"];
            var refreshToken = context.Request.Cookies["RefreshToken"];


            if (String.IsNullOrEmpty(accessToken) || String.IsNullOrEmpty(refreshToken))
            {
                throw new InvalidOperationException("No Token,Unauthorized Access");
            }

            var (principal, Validatetoken) = JwtServices.VerifyJwtToken(accessToken);
            var role = principal.FindFirst("Role")?.Value;
            var Email = principal.FindFirst("Email")?.Value;
            var id = principal.FindFirst("Id")?.Value;

            if (id == null || !(int.TryParse(id, out int Employeeid)))
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


            await next(context);
    
            //DateTime ExpiryDate = JwtServices.JwtTokenExpiresAt(accessToken);


            //if (ExpiryDate > DateTime.UtcNow)
            //{

            //    context.User = principal;

            //    await next(context);
            //    return;

            //}


            //throw new Exception("Access Token Expired,Unauthorized Access.");





        }



    }
}
