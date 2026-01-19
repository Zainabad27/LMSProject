using System.ComponentModel.DataAnnotations;

namespace LmsApp2.Api.DTOs
{
    public class CourseDto
    {


        [Required]
        public required string CourseName { get; set; }

        [Required]
        public required string BoardOrDepartment { get; set; }
 


    }
}
