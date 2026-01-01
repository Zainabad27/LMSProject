using LmsApp2.Api.DTOs;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;

namespace LmsApp2.Api.Repositories
{
    public class AssignmentRepo(LmsDatabaseContext dbcontext) : IAssignmentRepo
    {
        public async Task<Guid> UploadAssignment(AssignmentDto assignmentData, String FilePathOnServer,Guid TeacherId,String CourseName)
        {
            Assignment Ass = assignmentData.To_DBMODEL(TeacherId,CourseName);
            await dbcontext.Assignments.AddAsync(Ass);


            Assignmentquestion AssQ = assignmentData.AssQTo_DBMODEL(Ass.Assignmentid, FilePathOnServer);

            await dbcontext.Assignmentquestions.AddAsync(AssQ);

            return Ass.Assignmentid;

           
        }


        public async Task SaveChanges() {

            await dbcontext.SaveChangesAsync();

        
        }
    }
}
