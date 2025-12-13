using LmsApp2.Api.DTOs;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace LmsApp2.Api.Repositories
{
    public class ClassRepo(LmsDatabaseContext dbcontext) : IClassRepo
    {
        public async Task<Guid> AddClass(ClassDto Class, Guid SchoolId)
        {
            Class NewClass = Class.ToDb_Modle(SchoolId);
            await dbcontext.Classes.AddAsync(NewClass);

            return NewClass.Classid;

        }


        public async Task<Guid> GetClass(Guid SchoolId, string ClassSection, string ClassGrade)
        {

            return await dbcontext.Classes
             .Where(cls => (cls.Schoolid == SchoolId && cls.Classsection == ClassSection && cls.Classgrade == ClassGrade))
             .Select(cls => cls.Classid)
             .FirstOrDefaultAsync();

        }


        public async Task<Guid> GetACourse(Guid ClassId,string CourseName,string boardOrDepartment) {

          return await dbcontext.Courses
                .Where(crs => crs.Class == ClassId)
                .Select(crs => crs.Courseid)
                .FirstOrDefaultAsync();
        
        
        
        
        }



        public async Task<Guid> AddCourse(Guid ClassId, CourseDto CourseData)
        {
            Course NewCourse = CourseData.ToDbModel(ClassId);
            await dbcontext.Courses.AddAsync(NewCourse);


            return NewCourse.Courseid;


        }

        public async Task SaveChanges()
        {
            await dbcontext.SaveChangesAsync();
        }
    }
}
