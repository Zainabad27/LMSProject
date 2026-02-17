using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LmsApp2.Api.DTOs
{
    public class EmployeeDto : IEntity
    {
        [Required]

        public required string SchoolName { get; set; }
        [Required]
        public string EmployeeName { get; set; }

        // [Required]
        // public string EmployeeDesignation { get; set; }

        public string Religion { get; set; }

        public string Nationality { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        public string Contact { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }


        // [Required]
        public IFormFile photo { get; set; }


        // [Required]

        public IFormFile Cnic_Front { get; set; }


        // [Required]
        public IFormFile Cnic_Back { get; set; }



    }
}
