using LmsApp2.Api.DTOs;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.Models;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LmsApp2.Api.Repositories
{
    public class EmployeRepo(LmsDatabaseContext dbcontext) : IEmployeeRepo
    {
        public async Task<int> AddEmployee(EmployeeDto emp, int SchoolId,string SchoolName)
        {
            Employee employee = emp.To_DbModel(SchoolId);
            

            var EmployeeSavedInDatabase=await dbcontext.Employees.AddAsync(employee);  

           await  dbcontext.SaveChangesAsync();

            Employee emp2=EmployeeSavedInDatabase.Entity;
            return emp2.Employeeid;


        }

        public async Task<int> AddEmployeeDocuments(int EmpId, string PhotoPath, string CnicBackPath, string CnicFrontPath)
        {
            Employeedocument EmpDocs = new Employeedocument()
            {
                Employeeid=EmpId,
                Cnicfront=CnicFrontPath,
                Cnicback=CnicBackPath,
                Photo=PhotoPath,
                Createdat=DateTime.Now,
            };
            var DocsSavedInDatabse=await dbcontext.Employeedocuments.AddAsync(EmpDocs);
            await dbcontext.SaveChangesAsync();

            return DocsSavedInDatabse.Entity.Documentid;


        }

        public async Task<bool> EmployeeEmailAlreadyExists(string email)
        {
            string EmailExists=await dbcontext.Employeeaccountinfos.Where(emp=>emp.Email==email).Select(emp=>emp.Email).FirstOrDefaultAsync();
            if (EmailExists == null) {
                return false;
            
            
            }
            return true;

        }

        public async Task<int> MakeEmployeeUserAccount(EmployeeDto emp, int EmployeeIdOnEmployeesTable)
        {
            Employeeaccountinfo EmpAcc=new Employeeaccountinfo()
            {
                Email=emp.Email,
                Username= (EmployeeIdOnEmployeesTable.ToString()+"_"+emp.EmployeeName),
                Password =emp.Password.GetHashedPassword(),
                Createdat=DateTime.Now,
                Employeeid= EmployeeIdOnEmployeesTable,




            };

            var EmployeeAccountSavedInDatabase = await dbcontext.Employeeaccountinfos.AddAsync(EmpAcc);
            await dbcontext.SaveChangesAsync();


            return EmployeeAccountSavedInDatabase.Entity.Accountid;



        }
    }
}
