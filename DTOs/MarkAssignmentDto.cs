namespace LmsApp2.Api.DTOs
{
    public class MarkAssignmentDto
    {
        public Guid AssignmentId { get; set; }

        public Guid SubmissionId { get; set; }
        public decimal? Marks { get; set; }
    }
}