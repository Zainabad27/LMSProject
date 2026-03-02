namespace LmsApp2.Api.DTOs
{
    public class SendAllSubmissionsToFrontend
    {
        public Guid AssignmentId { get; set; }
        public string? StudentName { get; set; } = string.Empty;
        public string? SubmissionFilePathOnServer { get; set; } 
        public decimal?  MarksObtained { get; set; }

        public DateTime? SubmittedAt { get; set; }

        public Guid SubmissionId { get; set; }

        public bool IsChecked { get; set; }
    }
}