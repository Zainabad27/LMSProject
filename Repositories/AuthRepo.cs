using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Identity;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.Utilities;
using LmsApp2.Api.UtilitiesInterfaces;
using Microsoft.AspNetCore.Identity;
using Npgsql.Replication.PgOutput.Messages;

namespace LmsApp2.Api.Repositories
{
    internal class AuthRepo(UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager, LmsDatabaseContext dbcontext, IJwtServices JwtServices) : IAuthRepo
    {
        private async Task<Guid> UploadStudentDocuments(Guid StdId, Dictionary<string, string> Docs)
        {
            Studentdocument StdDocs = new()
            {

                Documentid = Guid.NewGuid(),
                Studentid = StdId,
                Cnicfrontorbform = Docs["cnicfront"],
                Cnicbackorbform = Docs["cnicback"],
                Photo = Docs["photo"],
                Createdat = DateTime.UtcNow


            };



            await dbcontext.Studentdocuments.AddAsync(StdDocs);

            return StdDocs.Documentid;
        }
        private async Task<Guid> UploadEmployeeDocuments(Guid EmpId, Dictionary<string, string> Docs)
        {

            Employeedocument EmpDocs = new()
            {
                Documentid = Guid.NewGuid(),
                Employeeid = EmpId,
                Cnicfront = Docs["cnicfront"],
                Cnicback = Docs["cnicback"],
                Photo = Docs["photo"],
                Createdat = DateTime.UtcNow,
            };
            var DocsSavedInDatabse = await dbcontext.Employeedocuments.AddAsync(EmpDocs);
            return DocsSavedInDatabse.Entity.Documentid;


        }
        public async Task<SendLoginDataToFrontend> Login(string email, string pass, string designation)
        {
            using var transaction = await dbcontext.Database.BeginTransactionAsync();

            var user = await _userManager.FindByEmailAsync(email) ?? throw new CustomException("Email not Found", 400);
            bool PasswordCorrect = await _userManager.CheckPasswordAsync(user, pass);

            bool IsDesignationCorrect = await _userManager.IsInRoleAsync(user, designation);


            if (!PasswordCorrect)
            {
                throw new CustomException("Invalid Password Given.", 400);
            }


            if (!IsDesignationCorrect)
            {
                throw new CustomException($"These Credentials are not of {designation}.", 400);
            }



            string AccessToken = JwtServices.GenerateAccessToken(designation, user);
            string RefreshToken = JwtServices.GenerateRefreshToken();


            user.RefreshToken = RefreshToken;
            user.TokenExpiry = DateTime.UtcNow.AddDays(3);



            await transaction.CommitAsync();

            return new SendLoginDataToFrontend()
            {
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
                UserId_InMainTable = user.UserId_InMainTable
            };


        }

        public async Task<(Guid EmployeeId, Guid DocId)> RegisterEmployee(EmployeeDto emp, Guid SchoolId, string designation, Dictionary<string, string> Docs)
        {
            using var transaction = await dbcontext.Database.BeginTransactionAsync();
            Employee employee = emp.To_DbModel(SchoolId,designation);



            // string LMAI=emp.Email;


            var user =await  _userManager.FindByEmailAsync(emp.Email);
            if (user != null)
            {
                throw new CustomException("Email Already in Use.", 400);

            }


            var AddingUser = new AppUser // this is Identity user while the above one was Main Db Emplloyee Table
            {
                Email = emp.Email,
                UserId_InMainTable = employee.Employeeid,
                UserName = emp.EmployeeName,
                PhoneNumber = emp.Contact
            };
            var result = await _userManager.CreateAsync(AddingUser, emp.Password);

            if (!result.Succeeded)
            {
                throw new CustomException($"Error occured while registering Employee. {string.Join(" ",result.Errors.Select(e => e.Description))}");
            }
            // adding user into that role that is given in the Designations we will check it that if the role exists in the service class

            bool roleExists = await _roleManager.RoleExistsAsync(designation);
            if (!roleExists)
            {
                throw new CustomException("This Designation does not exists in the System.", 400);
            }
            await _userManager.AddToRoleAsync(AddingUser, designation);
            var EmployeeSavedInDatabase = await dbcontext.Employees.AddAsync(employee);
            Guid DocId = await UploadEmployeeDocuments(EmployeeSavedInDatabase.Entity.Employeeid, Docs);





            await dbcontext.SaveChangesAsync();
            await transaction.CommitAsync();
            return (EmployeeSavedInDatabase.Entity.Employeeid, DocId);
        }


        public async Task<(Guid StudentId, Guid DocId)> RegisterStudent(StudentDto std, Guid SchoolId, Dictionary<string, string> docs)
        {


            // i have to delete the Photos From Server if Student is not registered.




            using var transaction = await dbcontext.Database.BeginTransactionAsync();
            var user = await _userManager.FindByEmailAsync(std.Email);
            if (user is not null)
            {
                await docs.DeleteFileFromServer();

                throw new CustomException("Email Already In use.", 400);
            }

            Student StudentInMainTable = std.ToDbModel(SchoolId); // extension method from student mapper
            // Guid StdId = Guid.NewGuid();


            AppUser studentAcc = new()
            {
                UserId_InMainTable = StudentInMainTable.Studentid,
                Email = std.Email,
                UserName = std.FirstName + " " + std.LastName,
                PhoneNumber = std.Contact,
            };


            var result = await _userManager.CreateAsync(studentAcc, std.Password);

            // Console.WriteLine(result.Errors);


            if (!result.Succeeded)
            {
                await docs.DeleteFileFromServer();
                throw new CustomException(
                 $"Error occurred while registering student. {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }



            bool StudentRoleExists = await _roleManager.RoleExistsAsync("Student");

            if (!StudentRoleExists)
            {
                await docs.DeleteFileFromServer();
                // await Rollback(studentAcc);

                throw new CustomException("Student Role Does not exists in the system.", 500);
            }


            var result2 = await _userManager.AddToRoleAsync(studentAcc, "Student");

            if (!result2.Succeeded)
            {
                await docs.DeleteFileFromServer();
                // await Rollback(studentAcc);
                throw new CustomException("Error Occured while Saving the student in the Database.", 500);
            }






            await dbcontext.Students.AddAsync(StudentInMainTable);

            Guid DocId = await UploadStudentDocuments(StudentInMainTable.Studentid, docs);

            await dbcontext.SaveChangesAsync();
            await transaction.CommitAsync();

            return (StudentInMainTable.Studentid, DocId);
        }

        public async Task SaveChanges()
        {
            await dbcontext.SaveChangesAsync();

        }
    }

}