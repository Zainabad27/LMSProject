using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;

namespace LmsApp2.Api.Services
{
    public class ClassService(ISchoolRepo schRepo, IClassRepo classRepo,IStudentRepo stdRepo) : IClassService
    {

        public async Task<Guid> EnrollStudent(EnrollClassDto EnrollmentData) {
            // first we have to check if the student exists and we have to check if class exists.
           var ClassInDb=await classRepo.GetClass(EnrollmentData.ClassId);
            if (ClassInDb == null) {

                throw new CustomException("This Class Does not Exists in the School.",400);
            
            }

            var StudentInDb=await stdRepo.GetStudent(EnrollmentData.StudentId);

            if (StudentInDb==null) {
                throw new CustomException("this Student does not exists in the database.");
            
            }

            if (StudentInDb.Isactive == false)
            {
                throw new CustomException("this student is currently not active.");
            }

            // since the entity is tracked by context i can assign the class id to it then call savechanges async.

            StudentInDb.Classid = ClassInDb.Classid;

            await stdRepo.SaveChanges();


            return ClassInDb.Classid;
        }
        public async Task<Guid> AddClass(ClassDto Class)
        {
            Guid SchoolId = await schRepo.GetSchoolByName(Class.SchoolName);
            if (SchoolId == Guid.Empty)
            {
                throw new CustomException($"{Class.SchoolName} School does not Exists in the database.",400);
            }

            // we have to check now that class already exists or not 


            Guid ClassAlreadyExists = await classRepo.GetClass(SchoolId, Class.ClassSection.ToUpper(), Class.ClassGrade);

            if (ClassAlreadyExists != Guid.Empty)
            {

                throw new CustomException($"Class {Class.ClassGrade} {Class.ClassSection} Already Exists in {Class.SchoolName}",400);


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
                throw new CustomException($"{courseData.SchoolName} School does not Exists in the database.",400);
            }
            Guid ClassID = await classRepo.GetClass(SchoolId, courseData.ClassSection.ToUpper(), courseData.ClassGrade);

            if (ClassID == Guid.Empty)
            {

                throw new CustomException($" Class {courseData.ClassGrade} {courseData.ClassSection.ToUpper()} does not exist in {courseData.SchoolName} yet.",400);


            }


            // now that we have checked the class does exists we have to check if that class already has this course registered for it.

            Guid CourseAlreadyExistsInThisClass = await classRepo.GetACourse(ClassID, courseData.CourseName, courseData.BoardOrDepartment);

            if (CourseAlreadyExistsInThisClass!=Guid.Empty) {

                throw new CustomException($"Course {courseData.CourseName} already exists in class {courseData.ClassGrade} {courseData.ClassSection.ToUpper()} of {courseData.SchoolName}",400);
            
            }

            Guid CourseId = await classRepo.AddCourse(ClassID, courseData);

            await classRepo.SaveChanges();

            return CourseId;

        }
    }
}
