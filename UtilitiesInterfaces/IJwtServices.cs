using LmsApp2.Api.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace LmsApp2.Api.UtilitiesInterfaces
{
    public interface IJwtServices
    {
        public string GenerateAccessToken(string Designation,AppUser _user);

        public string GenerateRefreshToken();

        public (ClaimsPrincipal principal, SecurityToken validateToken) VerifyJwtToken(string token);



        public DateTime JwtTokenExpiresAt(string Token);
    }
}
