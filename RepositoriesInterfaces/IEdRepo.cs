namespace LmsApp2.Api.RepositoriesInterfaces
{
    public interface IEdRepo
    {
        public Task<Guid> GetACourse(Guid CourseId);
    }
}
