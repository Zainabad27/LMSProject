using System.ComponentModel.DataAnnotations;

namespace LmsApp2.Api.DTOs
{
    public class GetAssignmentDto
    {
        [Required]
        public Guid CourseId { get; set; }
    }
}
