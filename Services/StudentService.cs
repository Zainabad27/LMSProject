using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.Utilities;
using LmsApp2.Api.UtilitiesInterfaces;
using LMSProject.DTOs;

namespace LmsApp2.Api.Services
{
    public class StudentService(IStudentRepo stdRepo, ISchoolRepo schRepo, IAssignmentRepo AssRepo, IClassRepo clsRepo, IFetchFileFromServer FetchFile, IWebHostEnvironment env) : IStudentService
    {


        public async Task<Guid> SubmitAssignment(AssignmentSubmissionDto Submission, Guid StudentId)
        {
            // first we have to validate that the student is enrolled in the class to which this assignment belongs.    

            // then we will check that the assignment is valid and exists in the Db.

            // then we will check the deadline of the assignment is not passed.

            // then we will save the file on the server and save the path in the Db against that student and assignment.

            Guid? StudentClassId = await stdRepo.GetStudentClass(StudentId);

            Guid? AssignmentClassId = await AssRepo.GetAssignmentClass(Submission.AssignmentId);

            if (AssignmentClassId == null)
            {
                throw new CustomException("No Assignment Found", 400);
            }
            if (AssignmentClassId != StudentClassId)
            {
                throw new CustomException("Student Is not Enrolled in this Class To Submit this Assignment.", 400);

            }

            await AssRepo.ValidAssignment(Submission.AssignmentId); // checks the deadline and existence of assignment.


            var DirectoryPath = Path.Combine(env.WebRootPath, "Submissions");



            string SubmissionFilePathOnServer = await Submission.SubmissionFile.UploadToServer(DirectoryPath);

            Guid SubmissionId = await stdRepo.SubmitAssignment(Submission, StudentId, SubmissionFilePathOnServer);

            await stdRepo.SaveChanges();


            return SubmissionId;
        }
        public async Task<Guid> AddStudent(StudentDto std)
        {
            Guid SchoolId = await schRepo.GetSchoolByName(std.SchoolName);

            if (SchoolId == Guid.Empty)
            {
                throw new CustomException("The School, Student is trying to register in, does not Exists.", 401);
            }



            Guid StudentId = await stdRepo.AddStudent(std, SchoolId);


            if (StudentId == Guid.Empty)
            {
                throw new Exception("Some error Occured In the Database while Saving the Student Data.");

            }


            Guid AccountId = await stdRepo.MakeStudentAccount(std, StudentId);




            var DirectoryPath = Path.Combine(env.WebRootPath, "Documents");


            string PhotoFilePathOnServer = await std.Photo.UploadToServer(DirectoryPath);
            string CnicFrontFilePathOnServer = await std.Cnic_Front.UploadToServer(DirectoryPath);
            string CnicBackFilePathOnServer = await std.Cnic_Back.UploadToServer(DirectoryPath);


            Guid DocId = await stdRepo.AddStudentDocuments(StudentId, PhotoFilePathOnServer, CnicBackFilePathOnServer, CnicFrontFilePathOnServer);

            await stdRepo.SaveChanges();

            return StudentId;

        }

        public async Task<List<AssignmentResponse>> GetAllAssignments(Guid StdId, Guid CourseId)
        {
            // first we have to check the students is real student 
            // then we have to check his class and in that class we will fetch the assignments for that particular course.
            // assignments are saved on the server in the form of jpeg or other file we have to fetch it from there DB only saves the Path of it on the server in the Database.

            Guid? StdClass = await stdRepo.GetStudentClass(StdId);


            List<AssignmentResponse> AssignmentIdAndData = await clsRepo.GetAllAssignmentsOfClass(StdClass, CourseId);

            return AssignmentIdAndData;

        }


        public async Task<byte[]> DownloadAssignment(Guid AssignmentId, Guid StdId)
        {
            // first we have to validate that the Student is Enrolled in the class to which this Assignment is 

            // then from Db we have to fetch Assignment path on server and then fetch file from server. 
            Guid? StdClassId = await stdRepo.GetStudentClass(StdId);

            var AssignmentClassId = await AssRepo.GetAssignmentClass(AssignmentId);

            if (AssignmentClassId == null)
            {
                throw new CustomException("No Assignment Found", 400);

            }


            if (AssignmentClassId != StdClassId)
            {
                throw new CustomException("Student Is not Enrolled in this Class To Access this Assignment.", 400);

            }


            var PathOnServer = await AssRepo.GetAssignmentPath(AssignmentId);

            if (PathOnServer == null)
            {
                throw new CustomException("Assignment Question Paper was not found in the DB.");

            }



            return FetchFile.FetchFile(PathOnServer);


        }

        public async Task<List<SendCoursesToFrontendDto>> GetStudentCourses(Guid StdId)
        {
            // first we have to  validtae that the student is real student and active in the system.
            // then we will fetch his class and then we will fetch all the courses assigned to that class.
            var (IsStudentPresentAndActive,ClassId) = await stdRepo.ValidStudent(StdId);       
            if (!IsStudentPresentAndActive)
            {
                throw new CustomException("No Active Student Found with this Id", 400);
            }

            if (ClassId == null)
            {
                throw new CustomException("This Student Is not enrolled in any Class currerntly", 400);
            }


            var CourseList = await stdRepo.GetStudentCourses(ClassId.Value);

            return CourseList;
        }

        public async Task<IEnumerable<SendStudentsToFrontendDto>> GetAllStudentsOfClass(Guid ClassId,int PageNumber,int PageSize)
        {
            // first we will check if the class exists
            // then we will fetch all the students of that class and return to the caller.
            Guid ClassID=await clsRepo.GetClass(ClassId);
            if(ClassID==Guid.Empty)
            {
                throw new CustomException("No Class Found with this Id", 400);
            }

            ICollection<SendStudentsToFrontendDto> students = await clsRepo.GetStudentsOfClass(ClassID,PageNumber,PageSize);


            if (students.Count==0)
            {
                throw new CustomException("No Students Found in this Class", 400);
                
            }


            return students;
        }
    }
}
