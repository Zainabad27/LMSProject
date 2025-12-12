using System.ComponentModel.DataAnnotations;

namespace LmsApp2.Api.DTOs
{
    public class CourseDto
    {


        [Required]
        public string CourseName { get; set; }

        [Required]
        public string BoardOrDepartment { get; set; }


        [Required]  

        public string ClassSection { get; set; }    


        [Required]
        public string ClassGrade { get; set; }

        
        [Required]
        public string SchoolName { get; set; }  




    }
}
