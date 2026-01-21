namespace LmsApp2.Api.DTOs
{
    public class SendTeachersToFrontend
    {
        public Guid TeacherId { get; set; }
        public string? TeacherName { get; set; }

        // public string? PhotoUrl { get; set; }

        public bool? IsActive { get; set; }

        // public string? Email { get; set; }


        // public string? PhoneNumber { get; set; }

        // public string? Address { get; set; }
    }
}