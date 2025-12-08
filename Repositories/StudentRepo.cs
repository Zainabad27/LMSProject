using LmsApp2.Api.DTOs;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LmsApp2.Api.Repositories
{
    public class StudentRepo(LmsDatabaseContext dbcontext) : IStudentRepo
    {
        public async Task<Guid> AddStudent(StudentDto std, Guid SchoolId)
        {
            Student Student = std.ToDbModel(SchoolId);
            var S = await dbcontext.Students.AddAsync(Student);

            return S.Entity.Studentid;


        }

        public async Task<Guid> MakeStudentAccount(StudentDto std, Guid StudentId)
        {

            Studentaccountinfo Acc = std.ToAccount(StudentId);
            var AccInDb = await dbcontext.Studentaccountinfos.AddAsync(Acc);
            return AccInDb.Entity.Accountid;

        }


        public async Task SaveChanges()
        {
            dbcontext.SaveChanges();
        }



        public async Task<(Guid StudentId, Guid AccountId)> AuthorizeStudent(string email, string Password)
        {
            var StudentAcc = await dbcontext.Studentaccountinfos.Where(std => std.Email == email).Select(std => std).FirstOrDefaultAsync();
            if (StudentAcc == null)
            {

                throw new Exception("Account was not Found in the Database.");

            }
            bool CorrectPassword = Password.VerifyHashedPassword(StudentAcc.Password);
            if (!CorrectPassword)
            {
                throw new Exception("Invalid Password");
            }



            var Std = await dbcontext.Students.FirstOrDefaultAsync(std => std.Studentid == StudentAcc.Studentid);
            if (Std == null)
            {
                throw new Exception("Student was not found in the Main Table");

            }

            if (!Std.Isactive)
            {

                throw new Exception("Student not Active.");

            }


            return (Std.Studentid, StudentAcc.Accountid);



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
            var IfSessionAlreadyExists = await dbcontext.Studentsessions.FirstOrDefaultAsync(sess => sess.Studentaccountid == StdAccId);


            if (IfSessionAlreadyExists != null)
            {

                IfSessionAlreadyExists.Refreshtoken = RefreshToken;
                IfSessionAlreadyExists.Expiresat = DateTime.UtcNow.AddDays(10);

                return IfSessionAlreadyExists.Sessionid;


            }

            // if session does not exists.


            Studentsession StdSession = new()
            {
                Sessionid = Guid.NewGuid(),
                Studentaccountid = StdAccId,
                Refreshtoken = RefreshToken,
                Createdat = DateTime.UtcNow,
                Expiresat = DateTime.UtcNow.AddDays(10),

            };



            await dbcontext.Studentsessions.AddAsync(StdSession);


            return StdSession.Sessionid;




        }
    }
}
