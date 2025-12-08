using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface ILoginService
    {
        public Task<Guid> AdminLogin(LoginDto LoginData,HttpContext context);
        public Task<Guid> TeacherLogin(LoginDto LoginData, HttpContext context);

        public Task<Guid> StudentLogin(LoginDto LoginData, HttpContext context);

    }
}
