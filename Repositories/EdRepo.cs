using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace LmsApp2.Api.Repositories
{
    public class EdRepo(LmsDatabaseContext dbcontext) : IEdRepo
    {
        public async Task<Guid> GetACourse(Guid CourseId)
        {
          return await dbcontext.Courses.Where(crs=>crs.Courseid==CourseId).Select(crs=>crs.Courseid).FirstOrDefaultAsync();
        }
    }
}
