using LmsApp2.Api.DTOs;
using LmsApp2.Api.Models;
using System.Net;

namespace LmsApp2.Api.Mappers
{
    public static class SchoolMapper
    {
        public static School To_DbModel(this SchoolDto School)
        {

            return new School()
            {
                Schoolname = School.SchoolName,
                Address = School.SchoolAddress,
                Createdat = DateTime.UtcNow
            };




        }



        public static SchoolDto To_Dto(this School School)
        {
            return new SchoolDto()
            {
                SchoolName=School.Schoolname,
                SchoolAddress= School.Address,

            };

        }




    }
}
