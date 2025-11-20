using LmsApp2.Api.UtilitiesInterfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LmsApp2.Api.Utilities
{
    public class JwtServices(IConfiguration config): IJwtServices
    {
        public string GenerateAccessTokes(int UserId, string Designation)
        {
            var claims = new List<Claim> {

                    new Claim(ClaimTypes.Role,Designation),
                    new Claim(ClaimTypes.NameIdentifier,UserId.ToString()),

            };

            string AccessToken = TokenGenerationForJwt(claims,3);

            return AccessToken;
        }
        public string GenerateRefreshTokes(int UserId, string Designation)
        {
            var claims = new List<Claim> {

                    new Claim(ClaimTypes.Role,Designation),
                    new Claim(ClaimTypes.NameIdentifier,UserId.ToString()),

            };

            string AccessToken = TokenGenerationForJwt(claims,15);

            return AccessToken;
        }

        private string TokenGenerationForJwt(List<Claim> claims,int TokenExpiry)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("AppSettingsForAdmin:Token")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var TokenDescriptor = new JwtSecurityToken(
                issuer: config.GetValue<string>("AppSettingsForAdmin:Issuer"),
                audience: config.GetValue<string>("AppSettingsForAdmin:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(TokenExpiry),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(TokenDescriptor);



        }
    }
}
