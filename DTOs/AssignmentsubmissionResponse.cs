namespace LmsApp2.Api.DTOs
{
    public class AssignmentsubmissionResponse
    {
        public Guid SubmissionId { get; set; }
        public Guid? AssignmentId { get; set; }
        public string? SubmissionFilePathOnServer { get; set; }
        public decimal? MarksObtained { get; set; }
        public string? Feedback { get; set; }
        public DateTime? SubmittedAt { get; set; }

    }
}