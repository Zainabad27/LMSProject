namespace LmsApp2.Api.DTOs
{
    public class SendTeachersToFrontend
    {
        public Guid TeacherId { get; set; }
        public string? TeacherName { get; set; }

        // public string? PhotoUrl { get; set; }

        public bool? IsActive { get; set; }

        public ICollection<CourseData> course = [];
    }



    public class CourseData
    {
        public string? CourseName { get; set; }

        public string? CourseClass { get; set; }


    }
}