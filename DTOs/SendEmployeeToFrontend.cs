using LmsApp2.Api.Models;

namespace LmsApp2.Api.DTOs
{
    public class SendEmployeeToFrontend
    {
        public Guid EmployeeId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Gender { get; set; }

        public string? ContactNumber { get; set; }

        public string? Address { get; set; }

        public DateOnly? DateOfJoining { get; set; }

        public bool? IsActive { get; set; }

        public ICollection<string> Documents = [];
    }
}
