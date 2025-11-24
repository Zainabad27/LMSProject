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
        public string GenerateAccessToken(int UserId, string Designation, string email)
        {
            var claims = new List<Claim> {

                    new Claim("Role",Designation),
                    new Claim("Id",UserId.ToString()),
                    new Claim("Email",email.ToString())



            };

            string AccessToken = TokenGenerationForJwt(claims, 1);

            return AccessToken;
        }
        public string GenerateRefreshToken()
        {


            string refreshToken = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();


            return refreshToken;
        }



        public (ClaimsPrincipal principal,SecurityToken validateToken) VerifyJwtToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("AppSettingsForAdmin:Token")));
            var tokenHandler = new JwtSecurityTokenHandler();


            var validationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,


                ValidIssuer = config["AppSettingsForAdmin:Issuer"],
                ValidateIssuer = true,     // set to true if you want strict issuer check


                ValidAudience = config["AppSettingsForAdmin:Audience"],
                ValidateAudience = true,   // set to true if you want strict audience check

                ValidateLifetime = true,    // checks expiry

            };



            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);



            return (principal, validatedToken);
        }


        public DateTime JwtTokenExpiresAt(string Token)
        {
            var TokenHandler= new JwtSecurityTokenHandler();

            SecurityToken DecodedToken = TokenHandler.ReadToken(Token);


            return DecodedToken.ValidTo;

        }

        private string TokenGenerationForJwt(List<Claim> claims, int TokenExpiry)
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
