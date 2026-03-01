namespace LmsApp2.Api.DTOs
{
    public class SendteacherAssignmentsToFrontend
    {
        public Guid AssignmentId { get; init; }

        public string CourseName { get; set; } = string.Empty;

        public DateTime Deadline { get; set; }  

        public int TotalSubmissions { get; set; }    

    }
}