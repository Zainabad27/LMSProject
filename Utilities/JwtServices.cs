using LmsApp2.Api.UtilitiesInterfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LmsApp2.Api.Utilities
{
    public class JwtServices(IConfiguration config) : IJwtServices
    {
        public string GenerateAccessToken(Guid UserId, string Designation, string email)
        {
            String GeneratingTokenFor = Designation + "s";
            string AccessToken = null!;
            var claims = new List<Claim> {

                    new(ClaimTypes.Role,Designation),
                    new("Id",UserId.ToString()),
                    new("Email",email.ToString())



            };

            AccessToken = TokenGenerationForJwt(claims, 1, GeneratingTokenFor);





            if (AccessToken == null)
            {
                throw new Exception("No Valid Audience was given for Generating AccessToken in JWT Services.");
            }



            return AccessToken;
        }
        public string GenerateRefreshToken()
        {


            string refreshToken = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();


            return refreshToken;
        }



        public (ClaimsPrincipal principal, SecurityToken validateToken) VerifyJwtToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!));
            var tokenHandler = new JwtSecurityTokenHandler();


            var validationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,


                ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                ValidateIssuer = true,     // set to true if you want strict issuer check


                ValidateAudience = false,   // set to true if you want strict audience check

                ValidateLifetime = true,    // checks expiry

            };



            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);



            return (principal, validatedToken);
        }


        public DateTime JwtTokenExpiresAt(string Token)
        {
            var TokenHandler = new JwtSecurityTokenHandler();

            SecurityToken DecodedToken = TokenHandler.ReadToken(Token);


            return DecodedToken.ValidTo;

        }

        private string TokenGenerationForJwt(List<Claim> claims, int TokenExpiry, string Audience)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var TokenDescriptor = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(TokenExpiry),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(TokenDescriptor);



        }
    }
}
