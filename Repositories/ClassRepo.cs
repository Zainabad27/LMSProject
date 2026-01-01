using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace LmsApp2.Api.Repositories
{

    public class ClassRepo(LmsDatabaseContext dbcontext) : IClassRepo
    {

        public async Task<List<String>> GetAllAssignmentsOfClass(Guid ClassId,String CourseName) {

           var AllAssignmentsOfClass= await dbcontext.Assignments.Include(ass=>ass.Assignmentquestion!.Content).AllAsync(ass=>ass.Classid==ClassId);

            throw new NotImplementedException();
        
        
        }

        public async Task<(bool,String)> CheckClassAndItsCourses(Guid ClassId, Guid CourseId)
        {
            var ClassInDb = await dbcontext.Classes.FirstOrDefaultAsync(cls => cls.Classid == ClassId);
            if (ClassInDb == null)
            {
                throw new CustomException("This Class Does Not Exists in our database", 400);

            }

            foreach (Course C in ClassInDb.Courses)
            {
                if (C.Courseid == CourseId)
                {
                    return (true,C.CourseName);
                }


            }


            return (false,"No Course Found");





        }
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


        public async Task<Class> GetClass(Guid ClassId)
        {
            return await dbcontext.Classes.FirstOrDefaultAsync(cls => cls.Classid == ClassId);

        }


        public async Task<Guid> GetACourse(Guid ClassId, string CourseName, string boardOrDepartment)
        {

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
