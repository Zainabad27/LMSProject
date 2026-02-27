namespace LmsApp2.Api.DTOs
{
    public class AssignmentResponse
    {
        public Guid AssignmentId { get; set; }

        public string CourseName { get; set; }

        public DateTime Deadline { get; set; }

        public decimal TotalMarks { get; set; } = (decimal)0.0;

        public bool IsSubmitted { get; set; } = false;

        public decimal MarksObtained { get; set; } = (decimal)0.0;
    }
}
