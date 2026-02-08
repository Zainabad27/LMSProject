using LmsApp2.Api.DTOs;
using LmsApp2.Api.Models;
using LmsApp2.Api.Utilities;
using System.Runtime.CompilerServices;

namespace LmsApp2.Api.Mappers
{
    public static class StudentMapper
    {
        public static Student ToDbModel(this StudentDto std, Guid SchoolId)
        {
            return new Student
            {
                Studentid = Guid.NewGuid(),
                StudentName = std.FirstName + " " + std.LastName,
                Gender = std.Gender,
                Nationality = std.Nationality,
                Religion = std.Relegion,
                Guardianname = std.GuardianFirstName + " " + std.GuardianLastName,
                Bloodgroup = std.BloodGroup,
                Isactive = true,
                Address = std.AddressFirstLine + "," + std.AddressSecondLine,
                Guardiancontact = std.Contact,
                Birthdate = std.BirthDate,
                Schoolid = SchoolId,
                Createdat = DateTime.UtcNow,

            };


        }
    }
}
