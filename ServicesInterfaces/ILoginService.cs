using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface ILoginService
    {
        public Task<string> AdminLogin(LoginDto LoginData);
    }
}
