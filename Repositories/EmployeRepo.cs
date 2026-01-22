using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LmsApp2.Api.Repositories
{
    public class EmployeRepo(LmsDatabaseContext dbcontext) : IEmployeeRepo
    {
        public async Task<Pagination<SendTeachersToFrontend>> GetAllTeachers(int pageNumber, int pageSize)
        {
            IQueryable<SendTeachersToFrontend> Query = dbcontext
            .Employees.Where(emp => emp.Employeedesignation == "Teacher")
            .Select(emp => new SendTeachersToFrontend
            {
                TeacherId = emp.Employeeid,
                TeacherName = emp.Employeename,
                IsActive = emp.Isactive,
            });
            return await Pagination<SendTeachersToFrontend>.CreateAsync(Query, pageNumber, pageSize);


        }
        public async Task<Guid> AssignCourse(Guid TeacherId, Guid CourseId)
        {
            var CourseInDb = await dbcontext.Courses.Where(crs => crs.Courseid == CourseId).FirstOrDefaultAsync();
            if (CourseInDb == null)
            {
                throw new CustomException("This Course Does not Exists in the Datase Currently.", 400);
            }

            CourseInDb.Teacher = TeacherId;

            return CourseInDb.Courseid;

        }
        public async Task<Guid> AddEmployee(EmployeeDto emp, Guid SchoolId, string designation)
        {
            Employee employee = emp.To_DbModel(SchoolId);

            employee.Employeedesignation = designation;


            var EmployeeSavedInDatabase = await dbcontext.Employees.AddAsync(employee);

            Employee emp2 = EmployeeSavedInDatabase.Entity;
            return emp2.Employeeid;


        }

        public async Task<Guid> AddEmployeeDocuments(Guid EmpId, string PhotoPath, string CnicBackPath, string CnicFrontPath)
        {
            Employeedocument EmpDocs = new Employeedocument()
            {
                Documentid = Guid.NewGuid(),
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

        public async Task<(Guid EmployeeAccountId, Guid EmployeeId)> AuthorizeEmployee(string email, string pass, string designation)
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
            if (UserInDatabase.Isactive != true)
            {
                throw new Exception("User Account Deleted");
            }
            if (UserInDatabase.Employeedesignation != designation)
            {
                throw new Exception("The designation of the user is not the same that was given in the Parameter.");
            }


            return (Employee.Accountid, UserInDatabase.Employeeid);  // returns the account Id of the employee Account to so that we can populate the Employee Session table.




        }
        public async Task<(Guid EmployeeAccountId, Guid EmployeeId)> AuthorizeEmployee(string email, string pass)
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
            if (UserInDatabase.Isactive != true)
            {
                throw new Exception("User Account Deleted");
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
                Accountid = Guid.NewGuid(),
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
                Sessionid = Guid.NewGuid(),

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
            Guid SessionId = await dbcontext.Employeeaccountinfos.Where(empAcc => empAcc.Employeeid == EmployeeId).Select(x => x.Employeesession != null ? x.Employeesession.Sessionid : Guid.Empty).FirstOrDefaultAsync();


            if (SessionId == Guid.Empty)
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


        public async Task<Guid> GetEmployee(Guid EmployeeId)
        {

            var EmployeeInDatabase = await dbcontext.Employees.FirstOrDefaultAsync(emp => emp.Employeeid == EmployeeId);
            if (EmployeeInDatabase == null)
            {
                return Guid.Empty;
            }

            if (EmployeeInDatabase.Isactive == false)
            {

                throw new CustomException("This Employee is Not Active Currently.", 400);


            }



            return EmployeeInDatabase.Employeeid;


        }






        public async Task<bool> ValidateEmployeeRefreshToken(Guid EmpId, string refreshToken)
        {
            var data = await dbcontext.Employeeaccountinfos.Where(empAcc => empAcc.Employeeid == EmpId).Select(x => new
            {

                refreshTokenExpiry = x.Employeesession != null ? x.Employeesession.Expiresat : DateTime.MinValue,
                refreshTokenIndataBase = x.Employeesession != null ? x.Employeesession.Refreshtoken : null


            }).FirstOrDefaultAsync();


            if (data == null || String.IsNullOrEmpty(data.refreshTokenIndataBase))
            {
                throw new Exception("Invalid refresh Token");
            }

            if (data.refreshTokenIndataBase != refreshToken)
            {
                throw new Exception("Invalid Refresh Token");
            }


            if (data.refreshTokenExpiry == DateTime.MinValue || data.refreshTokenExpiry < DateTime.UtcNow)
            {
                throw new Exception("Refresh Token Expired.");
            }


            return true;


        }

        public async Task<bool> CheckTeacherAndHisCourses(Guid TeacherId, Guid CourseId)
        {
            var EmployeeInDatabase = await dbcontext.Employees.Include(e => e.Courses).FirstOrDefaultAsync(emp => emp.Employeeid == TeacherId);

            if (EmployeeInDatabase == null)
            {
                throw new CustomException("This Teacher does not exists in our database.", 400);


            }
            if (EmployeeInDatabase.Isactive != true)
            {
                throw new CustomException("This Teacher is not Active Employee", 400);

            }

            if (EmployeeInDatabase.Employeedesignation != "Teacher")
            {
                throw new CustomException("This Employee is not a Teacher.", 400);
            }
            List<Course> cc = EmployeeInDatabase.Courses.ToList();

            foreach (Course C in EmployeeInDatabase.Courses)
            {
                if (C.Courseid == CourseId)
                {
                    return true;
                }



            }
            return false;


        }

        public async Task<SendEmployeeToFrontend?> GetEmployeeById(Guid employeeId)
        {
            return await dbcontext.Employees.Include(emp => emp.Employeeadditionaldocs).Where(emp => emp.Employeeid == employeeId).Select(emp => new SendEmployeeToFrontend
            {
                EmployeeId = emp.Employeeid,
                Name = emp.Employeename,
                Role = emp.Employeedesignation,
                IsActive = emp.Isactive,
                ContactNumber = emp.Contact,
                Address = emp.Address,
                DateOfJoining = emp.Createdat.HasValue ? DateOnly.FromDateTime(emp.Createdat.Value) : null,
                ProfilePictureUrl = emp.Employeedocuments != null ? emp.Employeedocuments.Photo ?? null : null,
                Email = emp.Employeeaccountinfo != null ? emp.Employeeaccountinfo.Email ?? null : null,
                Documents = emp.Employeeadditionaldocs
                                            .Select(d => d.Documentpath)
                                            .ToList()
            }).FirstOrDefaultAsync();

        }
    }
}
