namespace LmsApp2.Api.DTOs
{
    public class GetAssignment
    {
        public Guid AssignmentId
        {
            get; set;
        }

        public DateTime deadline
        {
            get; set;
        }


        public decimal? TotalMarks
        {
            get; set;
        }

        public Guid? upladedBy { get; set; } // Employeeid of the teacher who uploaded the assignment
        public Guid CourseId { get; set; }

    }
}