using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace LmsApp2.Api.Repositories
{
    public class AssignmentRepo(LmsDatabaseContext dbcontext) : IAssignmentRepo
    {

        public async Task<string?> GetAssignmentPath(Guid AssignmentId) {
        
          return await dbcontext.Assignments.Where(ass=>ass.Assignmentid==AssignmentId).Select(ass=>ass.Assignmentquestion!=null? ass.Assignmentquestion.Content:null).FirstOrDefaultAsync();    
        
        
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
            var ass=await dbcontext.Assignments.FirstOrDefaultAsync(ass=>ass.Assignmentid==AssignmentId);
    

            if (ass == null)
            {
                throw new CustomException("Assignment was not found in the Database",400);
            }

            if(ass.Deadline < DateTime.UtcNow)
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
                throw new CustomException("Assignment deadline was not set.",400);
            }


            return deadline;    
        }


        public async Task<Guid?> GetAssignmentClass(Guid AssignmentId)
        {

            var ClassId = await dbcontext.Assignments.Where(ass => ass.Assignmentid == AssignmentId).Select(ass => ass.Classid).FirstOrDefaultAsync();
            return ClassId;


        }


        public async Task SaveChanges()
        {

            await dbcontext.SaveChangesAsync();


        }
    }
}
