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
        public async Task<Guid> AddStudent(StudentDto std, Guid SchoolId)
        {
            Student Student = std.ToDbModel(SchoolId);
            var S = await dbcontext.Students.AddAsync(Student);

            return S.Entity.Studentid;


        }

        public async Task<Guid> MakeStudentAccount(StudentDto std, Guid StudentId)
        {
            throw new NotImplementedException();

            // Studentaccountinfo Acc = std.ToAccount(StudentId);
            // var AccInDb = await dbcontext.Studentaccountinfos.AddAsync(Acc);
            // return AccInDb.Entity.Accountid;

        }


        public async Task SaveChanges()
        {
            dbcontext.SaveChanges();
        }



        public async Task<(Guid StudentId, Guid AccountId)> AuthorizeStudent(string email, string Password)
        {
            throw new NotImplementedException();


            // var StudentAcc = await dbcontext.Studentaccountinfos.Where(std => std.Email == email).Select(std => std).FirstOrDefaultAsync();
            // if (StudentAcc == null)
            // {

            //     throw new Exception("Account was not Found in the Database.");

            // }
            // bool CorrectPassword = Password.VerifyHashedPassword(StudentAcc.Password);
            // if (!CorrectPassword)
            // {
            //     throw new Exception("Invalid Password");
            // }



            // var Std = await dbcontext.Students.FirstOrDefaultAsync(std => std.Studentid == StudentAcc.Studentid);
            // if (Std == null)
            // {
            //     throw new Exception("Student was not found in the Main Table");

            // }

            // if (!Std.Isactive)
            // {

            //     throw new Exception("Student not Active.");

            // }


            // return (Std.Studentid, StudentAcc.Accountid);



        }

        public async Task<Guid> AddStudentDocuments(Guid StdId, string PhotoPath, string CnicBackPath, string CnicFrontPath)
        {
            Studentdocument StdDocs = new Studentdocument()
            {
                Documentid = Guid.NewGuid(),
                Studentid = StdId,
                Createdat = DateTime.UtcNow,
                Photo = PhotoPath,
                Cnicbackorbform = CnicBackPath,
                Cnicfrontorbform = CnicFrontPath,



            };

            var DocsInDb = await dbcontext.Studentdocuments.AddAsync(StdDocs);

            return StdDocs.Documentid;
        }



        public async Task<Guid> PopulateStudentSession(Guid StdAccId, string RefreshToken)
        {
            throw new NotImplementedException();

            // var IfSessionAlreadyExists = await dbcontext.Studentsessions.FirstOrDefaultAsync(sess => sess.Studentaccountid == StdAccId);


            // if (IfSessionAlreadyExists != null)
            // {

            //     IfSessionAlreadyExists.Refreshtoken = RefreshToken;
            //     IfSessionAlreadyExists.Expiresat = DateTime.UtcNow.AddDays(10);

            //     return IfSessionAlreadyExists.Sessionid;


            // }

            // // if session does not exists.


            // Studentsession StdSession = new()
            // {
            //     Sessionid = Guid.NewGuid(),
            //     Studentaccountid = StdAccId,
            //     Refreshtoken = RefreshToken,
            //     Createdat = DateTime.UtcNow,
            //     Expiresat = DateTime.UtcNow.AddDays(10),

            // };



            // await dbcontext.Studentsessions.AddAsync(StdSession);


            // return StdSession.Sessionid;




        }
    }
}
