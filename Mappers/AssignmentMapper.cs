using LmsApp2.Api.DTOs;
using LmsApp2.Api.Models;

namespace LmsApp2.Api.Mappers
{
    public static class AssignmentMapper
    {
        public static Assignment To_DBMODEL(this AssignmentUploadDto assignmentData)
        {

            return new Assignment()
            {
                Assignmentid = Guid.NewGuid(),
                Employeeid = assignmentData.TeacherId,
                Createdat = DateTime.UtcNow,
                Classid = assignmentData.Class,
                Deadline = assignmentData.Deadline.ToDateTime(TimeOnly.MaxValue),
                Totalmarks = assignmentData.TotalMarks,

            };


        }
        public static Assignmentquestion AssQTo_DBMODEL(this AssignmentUploadDto assignmentData,Guid AssId,String FilePathOnServer)
        {

            return new Assignmentquestion()
            {
               Assignmentquestionid= Guid.NewGuid(),    
               Assignmentid=AssId,
               Createdat= DateTime.UtcNow,  
               Content=FilePathOnServer,


            };


        }
    }
}
