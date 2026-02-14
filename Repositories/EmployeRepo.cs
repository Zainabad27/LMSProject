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
    public class EmployeRepo(LmsDatabaseContext dbcontext, UserManager<AppUser> _userManager) : IEmployeeRepo
    {
        public async Task<Pagination<SendTeachersToFrontend>> GetAllTeachers(int pageNumber, int pageSize)
        {
            IQueryable<SendTeachersToFrontend> Query = dbcontext
            .Employees
            .Where(emp => emp.Employeedesignation == "Teacher")
            .Include(emp => emp.Courses)
            .Select(emp => new SendTeachersToFrontend
            {
                TeacherId = emp.Employeeid,
                TeacherName = emp.EmployeeName,
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
                throw new CustomException("This Employee is not Active Currently.", 400);
            }



            return EmployeeInDatabase.Employeeid;


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
            // throw new Exception("This func is to be written later.");
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
                Role = emp.Employeedesignation,
                IsActive = emp.Isactive,
                Name = emp.EmployeeName,
                // ContactNumber = emp.Contact,
                Address = emp.Address,
                DateOfJoining = emp.Createdat.HasValue ? DateOnly.FromDateTime(emp.Createdat.Value) : null,
                ProfilePictureUrl = emp.Employeedocuments != null ? emp.Employeedocuments.Photo ?? null : null,
                Documents = emp.Employeeadditionaldocs
                                            .Select(d => d.Documentpath)
                                            .ToList()
            }).FirstOrDefaultAsync();

        }

        public async Task<bool> EmployeeEmailAlreadyExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) == null;
        }

    }
}
