using LmsApp2.Api.Models;

namespace LmsApp2.Api.DTOs
{
    public class GetSubmissionFromDB(Assignmentsubmission submissionEntity)
    {
        public Assignmentsubmission SubmissionEntity { get; set; } = submissionEntity;
    }
}
