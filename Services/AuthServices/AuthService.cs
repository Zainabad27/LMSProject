using LmsApp2.Api.DTOs;
using LmsApp2.Api.Exceptions;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.ServicesInterfaces;
using LmsApp2.Api.Utilities;
using LmsApp2.Api.UtilitiesInterfaces;


namespace LmsApp2.Api.Services.AuthServices
{

    public class Login_Register_Service(IAuthRepo authRepo, ISchoolRepo schoolrepo, IWebHostEnvironment env) : ILogin_Register
    {
        public async Task<Guid> AdminLogin(LoginDto LoginData, HttpContext context)
        {

            // we have to first check the credentials of admin if its true then generate a jwt token that we have to

            SendLoginDataToFrontend data = await authRepo.Login(LoginData.Email, LoginData.Password, "Admin");

            // save access token in cokkies and refresh token in database and cookies, store session in the database , thats it.


            await authRepo.SaveChanges();



            var CookiesOptions = new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
            };

            context.Response.Cookies.Append("AccessToken", data.AccessToken, CookiesOptions);
            context.Response.Cookies.Append("RefreshToken", data.RefreshToken, CookiesOptions);


            return data.UserId_InMainTable;


        }
        public async Task<Guid> TeacherLogin(LoginDto LoginData, HttpContext context)
        {
            // we have to first check the credentials of admin if its true then generate a jwt token that we have to
            // save access token in cokkies and refresh token in database and cookies, store session in the database , thats it.


            SendLoginDataToFrontend data = await authRepo.Login(LoginData.Email, LoginData.Password, "Teacher");

            // custom auth code :  
            // var (EmployeeAccountId, EmployeeId) = await empRepo.AuthorizeEmployee(LoginData.Email, LoginData.Password, "Teacher");
            // string AccessToken = JwtServices.GenerateAccessToken(EmployeeId, "Teacher", LoginData.Email); // in access token we have put Employee Id in the Token payload not the Account Id.
            // string RefreshToken = JwtServices.GenerateRefreshToken();
            // generated the access token now i have to generate refresh token and put the both refresh and access token into the database.


            // Guid SessionId = await empRepo.PopulateEmployeeSession(EmployeeAccountId, RefreshToken);


            await authRepo.SaveChanges();

            var CookiesOptions = new CookieOptions
            {

                Secure = true,
                HttpOnly = true,



            };

            context.Response.Cookies.Append("AccessToken", data.AccessToken, CookiesOptions);
            context.Response.Cookies.Append("RefreshToken", data.RefreshToken, CookiesOptions);


            return data.UserId_InMainTable;

        }
        public async Task<Guid> StudentLogin(LoginDto LoginData, HttpContext context)
        {
            // we have to first check the credentials of admin if its true then generate a jwt token that we have to
            // save access token in cokkies and refresh token in database and cookies, store session in the database , thats it.



            SendLoginDataToFrontend data = await authRepo.Login(LoginData.Email, LoginData.Password, "Student");

            // var (StdId, StdAccId) = await stdRepo.AuthorizeStudent(LoginData.Email, LoginData.Password);
            // string AccessToken = JwtServices.GenerateAccessToken(StdId, "Student", LoginData.Email); // in access token we have put Employee Id in the Token payload not the Account Id.
            // string RefreshToken = JwtServices.GenerateRefreshToken();
            // // generated the access token now i have to generate refresh token and put the both refresh and access token into the database.


            // Guid SessionId = await stdRepo.PopulateStudentSession(StdAccId, RefreshToken);


            await authRepo.SaveChanges();

            var CookiesOptions = new CookieOptions
            {

                Secure = true,
                HttpOnly = true,



            };

            context.Response.Cookies.Append("AccessToken", data.AccessToken, CookiesOptions);
            context.Response.Cookies.Append("RefreshToken", data.RefreshToken, CookiesOptions);


            return data.UserId_InMainTable;


        }

        public async Task<Guid> RegisterEmployee(EmployeeDto emp, string Designation)
        {
            Guid SchoolId = await schoolrepo.GetSchoolByName(emp.SchoolName);
            if (SchoolId == Guid.Empty)
            {
                throw new Exception("School You are Registering For was not found in the Database.");
            }

            // now uploading the docs on the server.

            var DirectoryPath = Path.Combine(env.WebRootPath, "Documents");

            if (!Directory.Exists(DirectoryPath))
            {
                throw new CustomException("Some Internal Server Error Occured while Saving the Employee Data on the server.", 500);

            }


            string PhotoFilePathOnServer = await emp.photo.UploadToServer(DirectoryPath);
            string CnicFrontFilePathOnServer = await emp.photo.UploadToServer(DirectoryPath);
            string CnicBackFilePathOnServer = await emp.photo.UploadToServer(DirectoryPath);

            Dictionary<string, string> docs = new()
            {
                {"cnicback",CnicBackFilePathOnServer},
                {"cnicfront",CnicFrontFilePathOnServer},
                {"photo",PhotoFilePathOnServer}
            };



            // this repo runction adds employee data in the main table, Asp.NetUsers,Upload docs path in DB, Assign Role all in a single transaction.

            var (ReturnedEmpId, DocId) = await authRepo.RegisterEmployee(emp, SchoolId, Designation, docs);

            // await employeerepo.SaveChanges(); trasaction already commited inside this AddEmployee function.
            return ReturnedEmpId;
        }

        public async Task<Guid> RegisterStudent(StudentDto std)
        {
            Guid SchoolId = await schoolrepo.GetSchoolByName(std.SchoolName);

            if (SchoolId == Guid.Empty)
            {
                throw new CustomException("The School, Student is trying to register in, does not Exists.", 401);
            }


            var DirectoryPath = Path.Combine(env.WebRootPath, "Documents");


            string PhotoFilePathOnServer = await std.Photo.UploadToServer(DirectoryPath);
            string CnicFrontFilePathOnServer = await std.Cnic_Front.UploadToServer(DirectoryPath);
            string CnicBackFilePathOnServer = await std.Cnic_Back.UploadToServer(DirectoryPath);

            Dictionary<string, string> docs = new()
            {
                {"cnicback",CnicBackFilePathOnServer},
                {"cnicfront",CnicFrontFilePathOnServer},
                {"photo",PhotoFilePathOnServer}
            };


            var (StudentId, DocId) = await authRepo.RegisterStudent(std, SchoolId, docs);

            return StudentId;
        }
    }
}
