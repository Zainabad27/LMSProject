using LmsApp2.Api.DTOs;
using LmsApp2.Api.Models;
using System.Net;

namespace LmsApp2.Api.Mappers
{
    public static class EmployeMapper
    {


        public static Employee To_DbModel(this EmployeeDto emp, int SchoolId)
        {


            return new Employee()
            {


                Schoolid = SchoolId,
                Employeename = emp.EmployeeName,

                Employeedesignation = emp.EmployeeDesignation,

                Contact = emp.Contact,

                Address = emp.Address,

                Isactive = true,

                Religion = emp.Religion,

                Nationality = emp.Nationality,

                Createdat = DateTime.UtcNow,
            };
        }


        public static EmployeeDto To_Dto(this Employee emp, string Schoolname)
        {
            return new EmployeeDto()
            {

                SchoolName = Schoolname,
                EmployeeName = emp.Employeename,
                EmployeeDesignation = emp.Employeedesignation,
                Contact = emp.Contact,

                Religion = emp.Religion,

                Nationality = emp.Nationality,
                Address = emp.Address






            };
        }




    };
}







