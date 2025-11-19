using LmsApp2.Api.Repositories;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.Services;
using LmsApp2.Api.ServicesInterfaces;

namespace LmsApp2.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDI(this IServiceCollection services) {
            services.AddScoped<ISchoolService, SchoolService>();

            services.AddScoped<ISchoolRepo, SchoolRepo>();
            services.AddScoped<IEmployeeRepo, EmployeRepo>();
            services.AddScoped<IEmployeeService, EmployeeServices>();





            return services;
        
        }
    }
}
