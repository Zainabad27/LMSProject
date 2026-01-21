
namespace LmsApp2.Api.DTOs
{
    public class SendStudentsToFrontendDto
    {
        public string StudentName { get; set; } = null!;
        public Guid StudentId { get; set; }

        public string Gender { get; set; } = null!;

        public bool IsActive { get; set; }

        public string? Photo { get; set; }        


    }
}


