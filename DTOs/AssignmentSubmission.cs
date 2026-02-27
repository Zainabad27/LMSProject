

using System.ComponentModel.DataAnnotations;

namespace LMSProject.DTOs
{
    public class AssignmentSubmissionDto
    {
        [Required]
        public IFormFile SubmissionFile { get; set; }

        [Required]
        public Guid AssignmentId { get; set; }


    }
} 