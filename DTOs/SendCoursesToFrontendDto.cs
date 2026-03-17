
namespace LmsApp2.Api.DTOs
{
    public class SendCoursesToFrontendDto
    {
        public string CourseName { get; set; } = null!;
        public Guid CourseId { get; set; }
        public string Board { get; set; } = string.Empty;

    }
}

