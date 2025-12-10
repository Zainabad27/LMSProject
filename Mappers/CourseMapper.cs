using LmsApp2.Api.DTOs;
using LmsApp2.Api.Models;

namespace LmsApp2.Api.Mappers
{
    public static class CourseMapper
    {
        public static Course ToDbModel(this CourseDto CourseData, Guid ClassId)
        {

            return new Course()
            {
                Courseid=Guid.NewGuid(),    
                CourseName=CourseData.CourseName,   
                Boardordepartment=CourseData.BoardOrDepartment,
                Createdat=DateTime.UtcNow,
                Class = ClassId,
            };



        }

    }
}
