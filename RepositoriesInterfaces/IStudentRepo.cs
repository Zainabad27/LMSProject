namespace LmsApp2.Api.RepositoriesInterfaces
{
    public interface IStudentRepo
    {

        public Task<(Guid StudentId, Guid AccountId)> AuthorizeStudent(string email, string Password);


    }
}
