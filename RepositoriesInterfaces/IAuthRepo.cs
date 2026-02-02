using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.RepositoriesInterfaces
{



    public interface IAuthRepo
    {
        public Task<SendLoginDataToFrontend> Login(string email, string pass, string designation);



        public Task SaveChanges();
    }

}