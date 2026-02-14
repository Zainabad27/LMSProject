using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.Utilities;
using LMSProject.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LmsApp2.Api.Repositories
{

    public class StudentRepo(LmsDatabaseContext dbcontext) : IStudentRepo
    {
        public async Task<List<SendCoursesToFrontendDto>> GetStudentCourses(Guid ClassId)
        {
            var Courses = await dbcontext.Classes.Include(cls => cls.Courses).Where(cls => cls.Classid == ClassId).Select(cls => cls.Courses).FirstOrDefaultAsync();

            if (Courses == null || Courses.Count == 0)
            {
                throw new CustomException("No Courses Found for this Class", 404);
            }

            List<SendCoursesToFrontendDto> CourseList = [];


            foreach (Course C in Courses)
            {
                CourseList.Add(new SendCoursesToFrontendDto { CourseName = C.CourseName, CourseId = C.Courseid });
            }


            return CourseList;

        }

        public async Task<(bool ValidStudent,Guid? ClassId)> ValidStudent(Guid StudentId)
        {
            var StudentInDb = await dbcontext.Students.FirstOrDefaultAsync(std => std.Studentid == StudentId && std.Isactive == true);
            if (StudentInDb == null)
            {
                return (false, Guid.Empty);
            }
            return (true, StudentInDb.Classid);

        }

        public async Task<Guid> SubmitAssignment(AssignmentSubmissionDto Submission, Guid StudentId, string SubmissionFilePathOnServer)
        {
            Assignmentsubmission sub = Submission.To_DBMODEL(StudentId, SubmissionFilePathOnServer);
            await dbcontext.Assignmentsubmissions.AddAsync(sub);

            return sub.Assignmentsubmissionid;

        }
        public async Task<Guid?> GetStudentClass(Guid StdId)
        {
            // var ClassIdtesting = await dbcontext.Students.Where(s => s.Studentid == StdId && s.Isactive == true).Select(s => s.Classid).FirstOrDefaultAsync();

            var ClassId = await dbcontext.Students.Where(s => s.Studentid == StdId && s.Isactive == true).Select(s => s.Classid).FirstOrDefaultAsync();




            if (ClassId == null || ClassId == Guid.Empty)
            {
                throw new CustomException("This Student Is not Active Currently or Is not enrolled in any Class currerntly", 400);

            }



            return ClassId;

        }
        public async Task<Student?> GetStudent(Guid StudentId)
        {


            return await dbcontext.Students.FirstOrDefaultAsync(std => std.Studentid == StudentId);


        }
        


        public async Task SaveChanges()
        {
            dbcontext.SaveChanges();
        }

    }
}
