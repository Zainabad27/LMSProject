using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;

namespace LmsApp2.Api.Services
{
    public class ClassService(ISchoolRepo schRepo, IClassRepo classRepo, IStudentRepo stdRepo) : IClassService
    {

        public async Task<List<SendAllClassesToFrontendDto>> GetAllClasses(Guid SchoolId)
        {
            
            var ClassesInDb = await classRepo.GetAllClasses(SchoolId);
            return ClassesInDb;
        }

        public async Task<Guid> EnrollStudent(EnrollClassDto EnrollmentData)
        {
            // first we have to check if the student exists and we have to check if class exists.

            var IdOfClassInDb = await classRepo.GetClass(EnrollmentData.ClassId);
            if (IdOfClassInDb == Guid.Empty)
            {

                throw new CustomException("This Class Does not Exists in the School.", 400);

            }

            var StudentInDb = await stdRepo.GetStudent(EnrollmentData.StudentId);

            if (StudentInDb == null)
            {
                throw new CustomException("this Student does not exists in the database.", 400);

            }

            if (StudentInDb.Isactive == false)
            {
                throw new CustomException("this student is currently not active.", 400);
            }


            // and now we have to check if the student already exists in that class.

            if (StudentInDb.Classid == EnrollmentData.ClassId)
            {
                throw new CustomException("this Student is Already Enrolled in this class.");
            }

            // since the entity is tracked by context i can assign the class id to it then call savechanges async.

            StudentInDb.Classid = IdOfClassInDb;

            await stdRepo.SaveChanges();


            return IdOfClassInDb;

        }
        public async Task<Guid> AddClass(ClassDto Class)
        {
            Guid SchoolId = await schRepo.GetSchoolByName(Class.SchoolName);
            if (SchoolId == Guid.Empty)
            {
                throw new CustomException($"{Class.SchoolName} School does not Exists in the database.", 400);
            }

            // we have to check now that class already exists or not 


            Guid ClassAlreadyExists = await classRepo.GetClass(SchoolId, Class.ClassSection.ToUpper(), Class.ClassGrade);

            if (ClassAlreadyExists != Guid.Empty)
            {

                throw new CustomException($"Class {Class.ClassGrade} {Class.ClassSection} Already Exists in {Class.SchoolName}", 400);


            }


            Guid ClassId = await classRepo.AddClass(Class, SchoolId);

            await classRepo.SaveChanges();


            return ClassId;



        }

        public async Task<Guid> AssignCourseToAClass(Guid ClassId, Guid CourseId)
        {
            // first we have to check if class and course both exists.
            // then we have to check if that course is already assigned to that class.
            // then we have to assign the course to that class.
            Guid clsId = await classRepo.GetClass(ClassId);
            if (clsId == Guid.Empty)
            {
                throw new CustomException("This Class Does Not Exists in our database", 400);
            }

            var (CourseExists, CourseName) = await classRepo.CheckClassAndItsCourses(clsId, CourseId);

            if (CourseExists)
            {
                throw new CustomException($"This Course {CourseName} is already assigned to this class.", 400);
            }


            // now we can assign this course to this class.

            await classRepo.AssignCourseToAClass(CourseId, clsId);
            await classRepo.SaveChanges();
            return CourseId;
        }

        public async Task<Guid> AddCourse(CourseDto courseData)
        {
            // we have to do no validations basically.
            Guid AddedCourseId = await classRepo.AddCourse(courseData);
            await classRepo.SaveChanges();
            return AddedCourseId;
        }
    }
}
