using System.ComponentModel.DataAnnotations;

namespace LmsApp2.Api.DTOs
{
    public class ClassDto
    {
        [Required]
        public string SchoolName { get; set; }

        [Required]
         public string ClassGrade { get; set; }

        [Required]
        public string ClassSection { get; set; }








    }
}
