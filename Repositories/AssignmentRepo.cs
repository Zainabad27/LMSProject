using LmsApp2.Api.DTOs;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;

namespace LmsApp2.Api.Repositories
{
    public class AssignmentRepo(LmsDatabaseContext dbcontext) : IAssignmentRepo
    {
        public async Task<Guid> UploadAssignment(AssignmentDto assignmentData, String FilePathOnServer)
        {
            Assignment Ass = assignmentData.To_DBMODEL();
            await dbcontext.Assignments.AddAsync(Ass);


            Assignmentquestion AssQ = assignmentData.AssQTo_DBMODEL(Ass.Assignmentid, FilePathOnServer);


            return Ass.Assignmentid;

           
        }


        public async Task SaveChanges() {

            await dbcontext.SaveChangesAsync();

        
        }
    }
}
