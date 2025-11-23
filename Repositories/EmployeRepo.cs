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
        public async Task<int> AddEmployee(EmployeeDto emp, int SchoolId, string SchoolName)
        {
            Employee employee = emp.To_DbModel(SchoolId);


            var EmployeeSavedInDatabase = await dbcontext.Employees.AddAsync(employee);

            await dbcontext.SaveChangesAsync();

            Employee emp2 = EmployeeSavedInDatabase.Entity;
            return emp2.Employeeid;


        }

        public async Task<int> AddEmployeeDocuments(int EmpId, string PhotoPath, string CnicBackPath, string CnicFrontPath)
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
            await dbcontext.SaveChangesAsync();

            return DocsSavedInDatabse.Entity.Documentid;


        }

        public async Task<(int EmployeeAccountId,int EmployeeId)> AuthorizeEmployeeAsAdmin(string email, string pass)
        {
            var Employee = await dbcontext.Employeeaccountinfos.Where(emp => (emp.Email == email)).FirstOrDefaultAsync();
            if (Employee == null) { throw new Exception("No User Found for this Email."); }

            bool CorrectPassword = pass.VerifyHashedPassword(Employee.Password);
            if (!CorrectPassword)
            {
                throw new Exception("Invalid Password");
            }


            var EmployeeId = Employee.Employeeid;
            if (EmployeeId < 1)
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

        public async Task<bool> EmployeeEmailAlreadyExists(string email)
        {
            var EmailExists = await dbcontext.Employeeaccountinfos.Where(emp => emp.Email == email).Select(emp => emp.Email).FirstOrDefaultAsync();
            if (EmailExists == null)
            {
                return false;


            }
            return true;

        }







        public async Task<int> MakeEmployeeUserAccount(EmployeeDto emp, int EmployeeIdOnEmployeesTable)
        {
            Employeeaccountinfo EmpAcc = new Employeeaccountinfo()
            {
                Email = emp.Email,
                Username = (EmployeeIdOnEmployeesTable.ToString() + "_" + emp.EmployeeName),
                Password = emp.Password.GetHashedPassword(),
                Createdat = DateTime.UtcNow,
                Employeeid = EmployeeIdOnEmployeesTable,





            };

            var EmployeeAccountSavedInDatabase = await dbcontext.Employeeaccountinfos.AddAsync(EmpAcc);
            await dbcontext.SaveChangesAsync();


            return EmployeeAccountSavedInDatabase.Entity.Accountid;



        }

        public async Task<int> PopulateEmployeeSession(int employeeAccountId, string refreshToken, HttpContext Context)
        {
            if (employeeAccountId < 1 || String.IsNullOrEmpty(refreshToken))
            {
                throw new Exception("Invalid Employee Id or Invalid refresh Token");

            }

            
            Employeesession Session = new()
            {

                Employeeaccountid = employeeAccountId,
                Refreshtoken = refreshToken,
                Expiresat = DateTime.UtcNow.AddDays(10),
                Createdat = DateTime.UtcNow,


            };
            var SessionSavedInDatabase = await dbcontext.Employeesessions.AddAsync(Session);
            await dbcontext.SaveChangesAsync();

            return SessionSavedInDatabase.Entity.Sessionid;


        }

        public Task<int> ValidateEmployeeRefreshToken(int EmpId, string refreshToken)
        {
            dbcontext.Employeeaccountinfos.Where(empAcc=>empAcc.Employeeid== EmpId).
        }
    }
}
