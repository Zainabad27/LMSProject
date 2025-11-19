using System.ComponentModel.DataAnnotations;

namespace LmsApp2.Api.DTOs
{
    public class SchoolDto
    {
        [Required]
        public string SchoolName { get; set; }

        [Required]
        public string SchoolAddress { get; set; }

        
    }
}
