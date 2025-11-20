using System.Security.Claims;

namespace LmsApp2.Api.UtilitiesInterfaces
{
    public interface IJwtServices
    {
        public string GenerateAccessTokes(int UserId, string Designation);

        
    }
}
