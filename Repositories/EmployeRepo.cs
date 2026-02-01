using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Identity;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.Utilities;
using LmsApp2.Api.UtilitiesInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LmsApp2.Api.Repositories
{
    public class EmployeRepo(LmsDatabaseContext dbcontext, IJwtServices JwtServices, UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager) : IEmployeeRepo
    {
        public async Task<Pagination<SendTeachersToFrontend>> GetAllTeachers(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
            IQueryable<SendTeachersToFrontend> Query = dbcontext
            .Employees
            // .Where(emp => emp.Employeedesignation == "Teacher")
            .Select(emp => new SendTeachersToFrontend
            {
                TeacherId = emp.Employeeid,
                // TeacherName = emp.Employeename,
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
        public async Task<(Guid EmployeeId, Guid DocumentId)> AddEmployee(EmployeeDto emp, Guid SchoolId, string designation, Dictionary<string, string> Docs)
        {
           
            using var transaction = await dbcontext.Database.BeginTransactionAsync();
            Employee employee = emp.To_DbModel(SchoolId);
            // adding user to the Role(the designation) in identity
            var user = _userManager.FindByEmailAsync(emp.Email);
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
                throw new CustomException($"Error occured while registering Employee. {result.Errors.Select(e => e.Description)}");
            }
            // adding user into that role that is given in the Designations we will check it that if the role exists in the service class
            await _userManager.AddToRoleAsync(AddingUser, designation);
            var EmployeeSavedInDatabase = await dbcontext.Employees.AddAsync(employee);
            Guid DocId = await AddEmployeeDocuments(EmployeeSavedInDatabase.Entity.Employeeid, Docs);

            await transaction.CommitAsync();
            return (EmployeeSavedInDatabase.Entity.Employeeid, DocId);


        }

        public async Task<Guid> AddEmployeeDocuments(Guid EmpId, Dictionary<string, string> Docs)
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


        public async Task<SendLoginDataToFrontend> AuthorizeEmployeeAndDesignation(string email, string pass, string designation)
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
                throw new CustomException($"this employee is not {designation}.", 400);
            }

            // wrote Custom Auth but since has implemented Identity so commented Out. 
            // var (EmployeeAccountId, EmployeeId) = await empRepo.AuthorizeEmployee(LoginData.Email, LoginData.Password, "Admin");


            string AccessToken = JwtServices.GenerateAccessToken(user.UserId_InMainTable, designation, email); // in access token we have put Employee Id in the Token payload not the Account Id.
            string RefreshToken = JwtServices.GenerateRefreshToken();


            user.RefreshToken = RefreshToken;
            user.TokenExpiry = DateTime.Today.AddDays(3);



            await transaction.CommitAsync();





            return new SendLoginDataToFrontend()
            {
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
                UserId_InMainTable = user.UserId_InMainTable
            };


        }




        public async Task SaveChanges()
        {
            await dbcontext.SaveChangesAsync();
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
            throw new NotImplementedException();



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
            throw new Exception("This func is to be written later.");
            // if (EmployeeInDatabase.Employeedesignation != "Teacher")
            // {
            //     throw new CustomException("This Employee is not a Teacher.", 400);
            // }



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
                // Name = emp.Employeename,
                // Role = emp.Employeedesignation,
                IsActive = emp.Isactive,
                // ContactNumber = emp.Contact,
                Address = emp.Address,
                DateOfJoining = emp.Createdat.HasValue ? DateOnly.FromDateTime(emp.Createdat.Value) : null,
                ProfilePictureUrl = emp.Employeedocuments != null ? emp.Employeedocuments.Photo ?? null : null,
                // Email = emp.Employeeaccountinfo != null ? emp.Employeeaccountinfo.Email ?? null : null,
                Documents = emp.Employeeadditionaldocs
                                            .Select(d => d.Documentpath)
                                            .ToList()
            }).FirstOrDefaultAsync();

        }



        

        public async Task<bool> EmployeeEmailAlreadyExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) == null;
        }

        public async Task MakeEmployeeUserAccount(EmployeeDto emp, Guid EmployeeIdOnEmployeesTable)
        {
            var user = new AppUser
            {
                Email = emp.Email,
                UserId_InMainTable = EmployeeIdOnEmployeesTable,


            };


        }
    }
}
