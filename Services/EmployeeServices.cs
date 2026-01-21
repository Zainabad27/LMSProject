using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.Utilities;

namespace LmsApp2.Api.Services
{
    public class EmployeeServices(IEmployeeRepo employeerepo, IClassRepo ClsRepo, IAssignmentRepo assrepo, ISchoolRepo schoolrepo, IWebHostEnvironment env) : IEmployeeService
    {
        public async Task<Pagination<SendTeachersToFrontend>> GetAllTeachers(int PageNumber, int PageSize)
        {
            Pagination<SendTeachersToFrontend> TeachersList = await employeerepo.GetAllTeachers(PageNumber, PageSize);
            return TeachersList;
            
        }
        public async Task<List<(Guid AssignmentId,string CourseName)>> GetAssignmentsOfTeacher(Guid TeacherId, Guid CourseId)
        {
            // first we have to check that the teacher is assigned to that course or not

            // if assigned then we will fetch all the assignments uploaded by that teacher for that course. 

           bool TeachesThisSubject= await employeerepo.CheckTeacherAndHisCourses(TeacherId, CourseId);
            if (!TeachesThisSubject)
            {
                throw new CustomException("This Teacher is not assigned to this Course hence cannot fetch assignments for this Course.", 400);
            }

            List<(Guid AssignmentId, string CourseName)> AssignmentsList;
    
            AssignmentsList=await assrepo.GetAssignmentsOfTeacherForACourse(TeacherId, CourseId); 


            if(AssignmentsList.Count==0)
            {
                throw new CustomException("No Assignments were found for this Teacher for this Course.", 404);
            }

            return AssignmentsList;
            
        }


        public async Task<Guid> UploadAssignment(AssignmentDto assignmentData, Guid TeacherId)
        {
            // first we have to check the teacher is trying to upload the assignment for which course does he even teach it or not by course Id.
            // second we will check the class he is uploading assignment for does that course is assigned to that class or not
            // // we will also check if the employee id that has been given is the employee teacher as well.

            // then after validations we have to upload assigment 
            // first we have to upload assigment content on the server means saving the assigment file on the server 

            // second we have to save it in the database.


            bool Teacher_TeachesThisCourse = await employeerepo.CheckTeacherAndHisCourses(TeacherId, assignmentData.CourseId);
            if (!Teacher_TeachesThisCourse)
            {
                throw new CustomException("This Teacher Cannot Upload Assignment for this Course because this course is not assigned to them(they do not teach this course)", 400);
            }

            var (CourseisAssignedToClass, CourseName) = await ClsRepo.CheckClassAndItsCourses(assignmentData.Class, assignmentData.CourseId);

            if (!CourseisAssignedToClass)
            {
                throw new CustomException("This Course is not Assigned to this Class hence you cannot upload this assignment for this Class");

            }

            // validations have finished now we can simply upload that assigment on the server and then in the DB with refrence of that class and that teacher.

            var DirectoryPath = Path.Combine(env.WebRootPath, "Assignments");

            //string aa = env.WebRootPath; was debugging.

            if (!Directory.Exists(DirectoryPath))
            {
                throw new CustomException("Some Internal Server Error Occured while Saving the Assignment Data on the server.", 500);

            }


            String AssignmentPathOnServer = await assignmentData.AssignmentFile.UploadToServer(DirectoryPath);

            Guid AssignmentId = await assrepo.UploadAssignment(assignmentData, $"Assignments/{assignmentData.AssignmentFile.FileName}", TeacherId, CourseName);
            await assrepo.SaveChanges();

            return AssignmentId;



        }

        public async Task<Guid> AssignCourseToTeacher(Guid TeacherId, Guid CourseId)
        {

            // first we have to check if that Teacher and Course Both exists 
            // then in course we have to just put that teacher's id then that course is assigned to that teacher.


            Guid TeacherIdReturned = await employeerepo.GetEmployee(TeacherId);
            if (TeacherId == Guid.Empty)
            {
                throw new CustomException("Teacher Does not Exists in the Database.", 400);
            }

            // while assigning the course from dbmodel we are also checking in a single query that if coure exists or not if not then throwing error.

            Guid CourseIdReturned = await employeerepo.AssignCourse(TeacherIdReturned, CourseId);

            await employeerepo.SaveChanges();

            return CourseIdReturned;


        }
        public async Task<Guid> AddEmployee(EmployeeDto emp, String Designation)
        {
            Guid SchoolId = await schoolrepo.GetSchoolByName(emp.SchoolName);
            if (SchoolId == Guid.Empty)
            {
                throw new Exception("School You are Registering For was not found in the Database.");
            }



            Guid ReturnedEmpId = await employeerepo.AddEmployee(emp, SchoolId, Designation);

            // now we have to make Emplloyees Account too and return it too.
            bool UserEmailAlreadyExists = await employeerepo.EmployeeEmailAlreadyExists(emp.Email);
            if (UserEmailAlreadyExists)
            {

                throw new Exception("This Email is Already in use, Please Enter a different email.");
            }
            Guid EmployeeAccountId = await employeerepo.MakeEmployeeUserAccount(emp, ReturnedEmpId);



            // now we have to upload the necessary documents of Employee and also save it too Server
            Console.WriteLine($"this is the webroot path:===>>>{env.WebRootPath}");
            var DirectoryPath = Path.Combine(env.WebRootPath, "Documents");

            if (!Directory.Exists(DirectoryPath))
            {
                throw new CustomException("Some Internal Server Error Occured while Saving the Employee Data on the server.", 500);

            }

            string PhotoFilePathOnServer = await emp.photo.UploadToServer(DirectoryPath);
            string CnicFrontFilePathOnServer = await emp.photo.UploadToServer(DirectoryPath);
            string CnicBackFilePathOnServer = await emp.photo.UploadToServer(DirectoryPath);

            Guid DocumentId = await employeerepo.AddEmployeeDocuments(ReturnedEmpId, PhotoFilePathOnServer, CnicFrontFilePathOnServer, CnicBackFilePathOnServer);

            await employeerepo.SaveChanges();
            return ReturnedEmpId;

		// acha bro so the problem is that we have started using nvim for editing and currently we do not have any type of extensions 
		


        }
    }
}
