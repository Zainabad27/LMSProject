namespace LmsApp2.Api.RepositoriesInterfaces
{
    internal interface IGenericRepo<T> where T : class
    {
        Task<T> Get();
        Task<ICollection<T>> GetAll();
    }
}