using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace LmsApp2.Api.DTOs
{
    public class StudentDto : IEntity
    {
        [Required]
        public string SchoolName { get; set; }


        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public String GuardianFirstName { get; set; }
        [Required]
        public String GuardianLastName { get; set; }
        [Required]
        public String AddressFirstLine { get; set; }

        public String? AddressSecondLine { get; set; }
        [Required]
        public String Gender { get; set; }
        [Required]
        public DateOnly BirthDate { get; set; }

        public string Nationality { get; set; }
        [Required]
        public String Contact { get; set; }

        [Required]
        public string Relegion { get; set; }

        [Required]
        public string BloodGroup { get; set; }


        [Required]
        public IFormFile Photo { get; set; }


        [Required]

        public IFormFile Cnic_Front { get; set; }


        [Required]
        public IFormFile Cnic_Back { get; set; }



        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
