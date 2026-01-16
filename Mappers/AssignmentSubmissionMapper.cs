using LmsApp2.Api.Models;
using LMSProject.DTOs;

namespace LmsApp2.Api.Mappers
{
    public static class AssignmentSubmissionMapper
    {
        public static Assignmentsubmission To_DBMODEL(this AssignmentSubmissionDto submissionData, Guid StudentId, String FilePathOnServer)
        {

            return new Assignmentsubmission()
            {
                Assignmentsubmissionid = Guid.NewGuid(),
                Assignmentid = submissionData.AssignmentId,
                Studentid = StudentId,
                Content = FilePathOnServer,
                Marksscored = (decimal)0.0,
                Createdat = DateTime.UtcNow,

            };

        }

    }
}
