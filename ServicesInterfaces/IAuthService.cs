using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface IAuthService
    {
        public Task<string> AdminLogin(LoginDto LoginData);
    }
}
