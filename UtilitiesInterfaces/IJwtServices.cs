using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace LmsApp2.Api.UtilitiesInterfaces
{
    public interface IJwtServices
    {
        public string GenerateAccessToken(Guid UserId, string Designation, string email);

        public string GenerateRefreshToken();

        public (ClaimsPrincipal principal, SecurityToken validateToken) VerifyJwtToken(string token);



        public DateTime JwtTokenExpiresAt(string Token);
    }
}
