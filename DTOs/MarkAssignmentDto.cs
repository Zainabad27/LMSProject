namespace LmsApp2.Api.DTOs
{
    public class MarkAssignmentDto
    {
        public Guid AssignmentId { get; set; }

        public Guid SubmissionId { get; set; }
        public Guid StudentId { get; set; }
        public double Marks { get; set; }

        public string Feedback { get; set; } = string.Empty;
    }
}