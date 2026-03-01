using System.Data;
using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;
using Xunit.Sdk;

namespace LmsApp2.Api.Repositories
{
    public class AssignmentRepo(LmsDatabaseContext dbcontext) : IAssignmentRepo
    {
        public async Task<ICollection<SendteacherAssignmentsToFrontend>> GetAssignmentsOfTeacherForACourse(Guid TeacherId, Guid CourseId)
        {
            var assignments = await dbcontext.Assignments.Where(ass=>ass.Employeeid == TeacherId && ass.Courseid == CourseId).Select(ass=>new SendteacherAssignmentsToFrontend
            {
                AssignmentId = ass.Assignmentid,
                CourseName = ass.Coursename,
                Deadline = ass.Deadline,
                TotalSubmissions = ass.Assignmentsubmissions.Count,
            }).ToListAsync();

            return assignments;
        }
        public async Task<string?> GetAssignmentPath(Guid AssignmentId)
        {

            return await dbcontext.Assignments.Where(ass => ass.Assignmentid == AssignmentId).Select(ass => ass.Assignmentquestion != null ? ass.Assignmentquestion.Content : null).FirstOrDefaultAsync();

        }
        public async Task<Guid> UploadAssignment(AssignmentDto assignmentData, String FilePathOnServer, Guid TeacherId, String CourseName)
        {
            Assignment Ass = assignmentData.To_DBMODEL(TeacherId, CourseName);
            await dbcontext.Assignments.AddAsync(Ass);


            Assignmentquestion AssQ = assignmentData.AssQTo_DBMODEL(Ass.Assignmentid, FilePathOnServer);

            await dbcontext.Assignmentquestions.AddAsync(AssQ);

            return Ass.Assignmentid;


        }

        public async Task<bool> ValidAssignment(Guid AssignmentId)
        {
            var ass = await dbcontext.Assignments.FirstOrDefaultAsync(ass => ass.Assignmentid == AssignmentId) ?? throw new CustomException("Assignment was not found in the Database", 400);
            if (ass.Deadline < DateTime.UtcNow)
            {
                throw new CustomException("Assignment deadline has passed.");
            }

            return true;
        }

        public async Task<DateTime> GetAssignmentDeadline(Guid AssignmentId)
        {
            var deadline = await dbcontext.Assignments.Where(ass => ass.Assignmentid == AssignmentId).Select(ass => ass.Deadline).FirstOrDefaultAsync();

            if (deadline <= DateTime.MinValue)
            {
                throw new CustomException("Assignment deadline was not set.", 400);
            }


            return deadline;
        }


        public async Task<Guid?> GetAssignmentClass(Guid AssignmentId)
        {

            var ClassId = await dbcontext.Assignments.Where(ass => ass.Assignmentid == AssignmentId).Select(ass => ass.Classid).FirstOrDefaultAsync();
            return ClassId;


        }

        public async Task<List<AssignmentsubmissionResponse>> GetAllSubmittedAssignmentOfStudentForACourse(Guid studentId, Guid CourseId)
        {
            ICollection<Assignmentsubmission> SubmittedAssignments = await dbcontext.Assignmentsubmissions.Include(sub => sub.Assignment).Where(subm => subm.Studentid == studentId && subm.Assignment != null && subm.Assignment.Courseid == CourseId).ToListAsync();

            List<AssignmentsubmissionResponse> ResponseList = [];



            foreach (Assignmentsubmission ass in SubmittedAssignments)
            {
                ResponseList.Add(new AssignmentsubmissionResponse
                {
                    AssignmentId = ass.Assignmentid,
                    SubmissionId = ass.Assignmentsubmissionid,
                    SubmissionFilePathOnServer = ass.Content,
                    MarksObtained = ass.Marksscored,
                    SubmittedAt = ass.Createdat,

                });



            }



            return ResponseList;

        }

        public async Task GetSubmission(Guid submissionid)
        {
            var result = await dbcontext.Assignmentsubmissions.FirstOrDefaultAsync(sub => sub.Assignmentsubmissionid == submissionid) ?? throw new CustomException("Invalid submission Id.");

            throw new NotImplementedException();
        }
        public async Task<GetAssignment> GetAssignment(Guid AssignmentId)
        {
            var result = await dbcontext.Assignments.FirstOrDefaultAsync(ass => ass.Assignmentid == AssignmentId) ?? throw new CustomException("Assignment Not Found.", 400);

            return new GetAssignment()
            {
                AssignmentId = result.Assignmentid,
                deadline = result.Deadline,
                TotalMarks = result.Totalmarks,
                upladedBy = result.Employeeid,
                CourseId = result.Courseid
            };

        }

        public async Task SaveChanges()
        {

            await dbcontext.SaveChangesAsync();


        }
    }
}
