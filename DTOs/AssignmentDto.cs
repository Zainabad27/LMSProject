using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LmsApp2.Api.DTOs
{
    public class AssignmentDto
    {
        private DateOnly _deadline;

        [Required(ErrorMessage = "Enter the Course for which this assignment is destined.")]
        public Guid CourseId { get; set; }


        [Required(ErrorMessage = "Uploading Assignment File is Necessary")]
        public IFormFile AssignmentFile { get; set; }

        [Required(ErrorMessage = "Enter The Class for which this Assignment is destined.")]
        public Guid Class { get; set; }

        [Required]
        [Range(0, 100)]
        public int TotalMarks { get; set; }

        [Required(ErrorMessage = "Assignment Deadline date is Required.")]
        public DateOnly Deadline
        {
            get => this._deadline;
            set
            {

                // if (value < DateOnly.FromDateTime(DateTime.Now))
                // {
                //     throw new ArgumentException("DeadLine cannot be in the past.");
                // }
                // else
                // {
                    this._deadline = value;
                // }


            }
        }


    }
}
