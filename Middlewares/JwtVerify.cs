//using LmsApp2.Api.UtilitiesInterfaces;

//namespace LmsApp2.Api.Middlewares
//{
//    public class JwtVerify(RequestDelegate next)
//    {
//        public async Task InvokeAsync(HttpContext context, IJwtServices JwtServices)
//        {
//            var accessToken = context.Request.Cookies["AccessToken"];
//            var refreshToken = context.Request.Cookies["RefreshToken"];


//            if (String.IsNullOrEmpty(accessToken) || String.IsNullOrEmpty(refreshToken))
//            {
//                throw new InvalidOperationException("No Token,Unauthorized Access");
//            }

//            var (principal, Validatetoken) = JwtServices.VerifyJwtToken(accessToken);

//            var Email = principal.FindFirst("Email")?.Value;
//            var id = principal.FindFirst("Id")?.Value;

//            if (id == null || !(int.TryParse(id, out int Employeeid)))
//            {
//                throw new Exception("No Employee Id was given in AccessToken");

//            }


//            if (Email == null)
//            {
//                throw new Exception("No Email inside ");
//            }

//            context.User = principal;
//            await next(context);

//        }
//    }
//}
