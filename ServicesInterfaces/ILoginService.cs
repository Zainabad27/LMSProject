using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface ILoginService
    {
        public Task<int> AdminLogin(LoginDto LoginData,HttpContext context);
        public Task<int> TeacherLogin(LoginDto LoginData, HttpContext context);

    }
}
