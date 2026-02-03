using LmsApp2.Api.Repositories;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.Services;
using LmsApp2.Api.Services.AuthServices;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.Utilities;
using LmsApp2.Api.UtilitiesInterfaces;

namespace LmsApp2.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDI(this IServiceCollection services)
        {
            services.AddScoped<ISchoolService, SchoolService>();

            services.AddScoped<ISchoolRepo, SchoolRepo>();
            services.AddScoped<IEmployeeRepo, EmployeRepo>();
            services.AddScoped<IEmployeeService, EmployeeServices>();
            services.AddScoped<IJwtServices, JwtServices>();
            services.AddScoped<ILogin_Register, Login_Register_Service>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentRepo, StudentRepo>();
            services.AddScoped<IClassRepo, ClassRepo>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IAuthRepo,AuthRepo>();
            
            services.AddScoped<IAssignmentRepo, AssignmentRepo>();
            services.AddScoped<IFetchFileFromServer, FetchFileFromServer>();


            return services;

        }
    }
}
