namespace LmsApp2.Api.DTOs
{
    public class SendteacherAssignmentsToFrontend
    {
        public Guid AssignmentId { get; init; }

        public string CourseName { get; set; } = string.Empty;

    }
}