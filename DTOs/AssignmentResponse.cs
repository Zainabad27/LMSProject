namespace LmsApp2.Api.DTOs
{
    public class AssignmentResponse
    {
        public Guid AssignmentId { get; set; }

        public string CourseName { get; set; }

        public DateTime Deadline { get; set; }

        public decimal TotalMarks { get; set; } = (decimal)0.0;

        public bool IsSubmitted { get; set; } = false;

        public Guid? SubmissionId  { get; set; }    

        public decimal MarksObtained { get; set; } = (decimal)0.0;

        public string? SubmissionFilePath { get; set; }
    }
}
