using LmsApp2.Api.DTOs;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;

namespace LmsApp2.Api.Services
{
    public class ClassService(ISchoolRepo schRepo, IClassRepo classRepo) : IClassService
    {
        public async Task<Guid> AddClass(ClassDto Class)
        {
            Guid SchoolId = await schRepo.GetSchoolByName(Class.SchoolName);
            if (SchoolId == Guid.Empty)
            {
                throw new Exception($"{Class.SchoolName} School does not Exists in the database.");
            }


            Guid ClassId = await classRepo.AddClass(Class, SchoolId);

            await classRepo.SaveChanges();


            return ClassId;



        }

        public async Task<Guid> AddCourseToAClass(CourseDto courseData)
        {
            Guid SchoolId = await schRepo.GetSchoolByName(courseData.SchoolName);
            if (SchoolId == Guid.Empty)
            {
                throw new Exception($"{courseData.SchoolName} School does not Exists in the database.");
            }
            Guid ClassID = await classRepo.GetClass(SchoolId, courseData.ClassSection, courseData.ClassGrade);

            if (ClassID == Guid.Empty)
            {

                throw new Exception($"{courseData.ClassSection}{courseData.ClassGrade} does not exist in {courseData.SchoolName} yet.");


            }


            Guid CourseId = await classRepo.AddCourse(ClassID, courseData);



            await classRepo.SaveChanges();




            return CourseId;

        }
    }
}
