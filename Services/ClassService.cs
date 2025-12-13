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

            // we have to check now that class already exists or not 


            Guid ClassAlreadyExists = await classRepo.GetClass(SchoolId, Class.ClassSection.ToUpper(), Class.ClassGrade);

            if (ClassAlreadyExists != Guid.Empty)
            {

                throw new Exception($"Class {Class.ClassGrade} {Class.ClassSection} Already Exists in {Class.SchoolName}");


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
            Guid ClassID = await classRepo.GetClass(SchoolId, courseData.ClassSection.ToUpper(), courseData.ClassGrade);

            if (ClassID == Guid.Empty)
            {

                throw new Exception($" Class {courseData.ClassGrade} {courseData.ClassSection} does not exist in {courseData.SchoolName} yet.");


            }


            // now that we have checked the class does exists we have to check if that class already has this course registered for it.

            Guid CourseAlreadyExistsInThisClass = await classRepo.GetACourse(ClassID, courseData.CourseName, courseData.BoardOrDepartment);

            if (CourseAlreadyExistsInThisClass!=Guid.Empty) {

                throw new Exception($"Course {courseData.CourseName} already exists in class {courseData.ClassGrade} {courseData.ClassSection} of {courseData.SchoolName}");
            
            }

            Guid CourseId = await classRepo.AddCourse(ClassID, courseData);

            await classRepo.SaveChanges();

            return CourseId;

        }
    }
}
