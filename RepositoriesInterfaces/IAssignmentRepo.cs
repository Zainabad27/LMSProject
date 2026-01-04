using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.RepositoriesInterfaces
{
    public interface IAssignmentRepo
    {
        public Task<string?> GetAssignmentPath(Guid AssignmentId);
        public Task<Guid> UploadAssignment(AssignmentDto assignmentData, String FilePathOnServer, Guid TeacherId, String CourseName);

        public Task SaveChanges();

        public Task<Guid?> GetAssignmentClass(Guid AssignmentId);


    }
}
