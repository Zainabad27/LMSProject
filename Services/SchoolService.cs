using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.Mappers;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.SeedData;
using LmsApp2.Api.ServicesInterfaces;

namespace LmsApp2.Api.Services
{
    public class SchoolService(ISchoolRepo schoolrepo) : ISchoolService
    {

        public async Task<Guid> AddSchool(SchoolDto SchoolData)
        {
            // we will chek if that school already exists.
            Guid Schoolid = await schoolrepo.GetSchoolByName(SchoolData.SchoolName);
            if (Schoolid != Guid.Empty)
            {
                throw new CustomException("School name Already exists", 400);
            }
            // when we add an school we make a default account of an Admin.


            Guid SchoolId = await schoolrepo.AddSchool(SchoolData);


            // making a default Admin Account

            var DefaultAdminAccount = await schoolrepo.SeedInitialData(SchoolId); // returns the defaulAdmin Account.


            try
            {
                await schoolrepo.SaveChanges();
            }
            catch (System.Exception)
            {
                if (DefaultAdminAccount != null) await schoolrepo.Rollback(DefaultAdminAccount); // deletes the Identity Account Data if School Is not registered. (transaction Rollback).

                throw;
            }
            return SchoolId;


        }

    }
}
