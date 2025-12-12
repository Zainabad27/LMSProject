using LmsApp2.Api.DTOs;
using LmsApp2.Api.Models;
using System.Runtime.CompilerServices;

namespace LmsApp2.Api.Mappers
{
    public static class ClassMapper
    {
        public static Class ToDb_Modle(this ClassDto ClassData,Guid SchoolId) {

            return new Class() {
            Classid=Guid.NewGuid(), 
            Classgrade=ClassData.ClassGrade,
            Classsection=ClassData.ClassSection.ToUpper(),    
            Createdat=DateTime.UtcNow,
            Schoolid=SchoolId,
            };
        
        
        
        
        
        }

    }
}
