using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.RepositoriesInterfaces
{
    public interface IAssignmentRepo
    {
        public Task<Guid> UploadAssignment(AssignmentDto assignmentData, String FilePathOnServer);

        public Task SaveChanges();
        
        }
}
