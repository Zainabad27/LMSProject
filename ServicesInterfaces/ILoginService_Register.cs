using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface ILogin_Register
    {
        public Task<Guid> AdminLogin(LoginDto LoginData, HttpContext context);
        public Task<Guid> TeacherLogin(LoginDto LoginData, HttpContext context);

        public Task<Guid> StudentLogin(LoginDto LoginData, HttpContext context);


        public Task<Guid> RegisterEmployee(EmployeeDto emp, string designation);

        public Task<Guid> RegisterStudent(StudentDto std);

    }
}
