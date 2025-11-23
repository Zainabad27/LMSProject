using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface ITokenServices
    {
        public Task<int> RefreshAccesToken(RefreshAccessTokenDto RefreshToken,HttpContext context);
    }
}
