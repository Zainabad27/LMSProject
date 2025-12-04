using LmsApp2.Api.DTOs;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LmsApp2.Api.Repositories
{
    public class EmployeRepo(LmsDatabaseContext dbcontext) : IEmployeeRepo
    {
        public async Task<Guid> AddEmployee(EmployeeDto emp, Guid SchoolId, string SchoolName)
        {
            Employee employee = emp.To_DbModel(SchoolId);


            var EmployeeSavedInDatabase = await dbcontext.Employees.AddAsync(employee);

            //await dbcontext.SaveChangesAsync();

            Employee emp2 = EmployeeSavedInDatabase.Entity;
            return emp2.Employeeid;


        }

        public async Task<Guid> AddEmployeeDocuments(Guid EmpId, string PhotoPath, string CnicBackPath, string CnicFrontPath)
        {
            Employeedocument EmpDocs = new Employeedocument()
            {
                Employeeid = EmpId,
                Cnicfront = CnicFrontPath,
                Cnicback = CnicBackPath,
                Photo = PhotoPath,
                Createdat = DateTime.UtcNow,
            };
            var DocsSavedInDatabse = await dbcontext.Employeedocuments.AddAsync(EmpDocs);
            //await dbcontext.SaveChangesAsync();

            return DocsSavedInDatabse.Entity.Documentid;


        }

        public async Task<(Guid EmployeeAccountId, Guid EmployeeId)> AuthorizeEmployeeAsAdmin(string email, string pass)
        {
            var Employee = await dbcontext.Employeeaccountinfos.Where(emp => (emp.Email == email)).FirstOrDefaultAsync();
            if (Employee == null) { throw new Exception("No User Found for this Email."); }

            bool CorrectPassword = pass.VerifyHashedPassword(Employee.Password);
            if (!CorrectPassword)
            {
                throw new Exception("Invalid Password");
            }


            var EmployeeId = Employee.Employeeid;
            if (EmployeeId == null)
            {
                throw new Exception("Invalid Credentials");
            }

            var UserInDatabase = await dbcontext.Employees.FirstOrDefaultAsync(emp => emp.Employeeid == EmployeeId);
            if (UserInDatabase == null)
            {
                throw new Exception("User does not exists");
            }
            if (UserInDatabase?.Isactive != true)
            {
                throw new Exception("User Account Deleted");
            }
            if (UserInDatabase.Employeedesignation != "Admin")
            {
                throw new Exception("User Is not an Admin");
            }


            return (Employee.Accountid, UserInDatabase.Employeeid);  // returns the account Id of the employee Account to so that we can populate the Employee Session table.




        }
        public async Task<(Guid EmployeeAccountId, Guid EmployeeId)> AuthorizeEmployeeAsTeacher(string email, string pass)
        {
            var Employee = await dbcontext.Employeeaccountinfos.Where(emp => (emp.Email == email)).FirstOrDefaultAsync();
            if (Employee == null) { throw new Exception("No User Found for this Email."); }

            bool CorrectPassword = pass.VerifyHashedPassword(Employee.Password);
            if (!CorrectPassword)
            {
                throw new Exception("Invalid Password");
            }


            var EmployeeId = Employee.Employeeid;
            if (EmployeeId ==null)
            {
                throw new Exception("Invalid Credentials");
            }

            var UserInDatabase = await dbcontext.Employees.FirstOrDefaultAsync(emp => emp.Employeeid == EmployeeId);
            if (UserInDatabase == null)
            {
                throw new Exception("User does not exists");
            }
            if (UserInDatabase?.Isactive != true)
            {
                throw new Exception("User Account Deleted");
            }
            if (UserInDatabase.Employeedesignation != "Teacher")
            {
                throw new Exception("User Is not a Teacher");
            }


            return (Employee.Accountid, UserInDatabase.Employeeid);  // returns the account Id of the employee Account to so that we can populate the Employee Session table.




        }

        public async Task<bool> EmployeeEmailAlreadyExists(string email)
        {
            var EmailExists = await dbcontext.Employeeaccountinfos.Where(emp => emp.Email == email).Select(emp => emp.Email).FirstOrDefaultAsync();
            if (EmailExists == null)
            {
                return false;


            }
            return true;

        }

        public async Task<Guid> MakeEmployeeUserAccount(EmployeeDto emp, Guid EmployeeIdOnEmployeesTable)
        {
            Employeeaccountinfo EmpAcc = new Employeeaccountinfo()
            {
                Email = emp.Email,
                Password = emp.Password.GetHashedPassword(),
                Createdat = DateTime.UtcNow,
                Employeeid = EmployeeIdOnEmployeesTable,


            };

            var EmployeeAccountSavedInDatabase = await dbcontext.Employeeaccountinfos.AddAsync(EmpAcc);
        


            return EmployeeAccountSavedInDatabase.Entity.Accountid;



        }

        public async Task<Guid> PopulateEmployeeSession(Guid employeeAccountId, string refreshToken)
        {
            if (String.IsNullOrEmpty(refreshToken))
            {
                throw new Exception("Invalid refresh Token");

            }
            // if session exists already so we just have to update it 

            var session = await dbcontext.Employeesessions.FirstOrDefaultAsync(e => e.Employeeaccountid == employeeAccountId);
            if (session != null)
            {
                session.Expiresat = DateTime.UtcNow.AddDays(10);
                session.Refreshtoken = refreshToken;
                //await dbcontext.SaveChangesAsync();


                return session.Sessionid;
            }

            Employeesession Session = new()
            {

                Employeeaccountid = employeeAccountId,
                Refreshtoken = refreshToken,
                Expiresat = DateTime.UtcNow.AddDays(10),
                Createdat = DateTime.UtcNow,


            };
            var SessionSavedInDatabase = await dbcontext.Employeesessions.AddAsync(Session);
            //await dbcontext.SaveChangesAsync();

            return SessionSavedInDatabase.Entity.Sessionid;


        }

        public async Task SaveChanges()
        {
            await dbcontext.SaveChangesAsync();
        }

        public async Task<Guid> UpdateEmployeeSession(Guid EmployeeId, string refreshToken)
        {
            int SessionId = await dbcontext.Employeeaccountinfos.Where(empAcc => empAcc.Employeeid == EmployeeId).Select(x => x.Employeesession != null ? x.Employeesession.Sessionid : 0).FirstOrDefaultAsync();


            if (SessionId < 1)
            {
                throw new Exception("Employee Account Not Found");
            }

            var sessionData = await dbcontext.Employeesessions.FirstOrDefaultAsync(session => session.Sessionid == SessionId);

            if (sessionData == null)
            {
                throw new Exception("Employee Session Data Was not found in the database.");
            }

            sessionData.Refreshtoken = refreshToken;
            sessionData.Expiresat = DateTime.UtcNow.AddDays(10);

            return SessionId;





        }

        public async Task<bool> ValidateEmployeeRefreshToken(Guid EmpId, string refreshToken)
        {
            var data = await dbcontext.Employeeaccountinfos.Where(empAcc => empAcc.Employeeid == EmpId).Select(x => new
            {

                refreshTokenExpiry = x.Employeesession != null ? x.Employeesession.Expiresat : DateTime.UtcNow.AddDays(-3),
                refreshTokenIndataBase = x.Employeesession != null ? x.Employeesession.Refreshtoken : "No refreshToken"


            }).FirstOrDefaultAsync();


            if (data == null || String.IsNullOrEmpty(data.refreshTokenIndataBase))
            {
                throw new Exception("Invalid refresh Token");
            }

            if (data.refreshTokenIndataBase != refreshToken)
            {
                throw new Exception("Invalid Refresh Token");
            }


            if (data.refreshTokenExpiry < DateTime.UtcNow)
            {
                throw new Exception("Refresh Token Expired.");
            }


            return true;

        }
    }
}
