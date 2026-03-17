[33md28eab6[m[33m ([m[1;36mHEAD[m[33m -> [m[1;32mmain[m[33m, [m[1;31morigin/main[m[33m, [m[1;31morigin/HEAD[m[33m)[m get all courses of school api written, have to implement it in FE.
[33m7c8861a[m Delete Class API and FE Implementation done.
[33mc38388e[m added the fields for the add Class form on the pop up now ill have to write the delete Class api in the backend.
[33md04fc4a[m first i'll have to write a delete class api then implement it on the dlete button on class card + on add class button a form is opening up its field I have to write those fields.
[33m9abd709[m made the class card now gonna connect it to the admin dashboard and also have to send the CourseId while rendering the Classpage for admin in AdminClasses page
[33m69a4115[m added dialog boxes for dashboard. now starting to make the pages.
[33m32e767e[m now returning user id as well from getuser api
[33m4aeaab2[m starting to write dashboard for every role seperately.
[33m1283bbf[m add getUser Api and now starting to write custom hook in react to get user
[33m988b985[m configured Axios to send credentials too to the backend.
[33mef5c94e[m .
[33m1d51751[m browser is not saving the token in cookies after logging in the app. have to debug this issue. or either browser is not sending the cookies while logged in.
[33m3478e6a[m Added Layout of the App, Header and Footer. Now start to work on the dashboard.
[33m8c2dec5[m start to make an layout for the app and start to work on the dashboard.
[33m0768f77[m resolved CORS, now the axios request is working just fine from the frontend.
[33mb1b5438[m CORS error while sending req from the frontend.
[33m0597721[m Trying to send a req using axios in the backend Network Error is occuring.
[33mc32f1be[m login and signup page done now we have to connect it to the backend.
[33mba54bd8[m cleanup
[33m730b765[m signup page added. routing not done properly.
[33m35351f7[m add login box for role selection page and now working on the login page
[33me748764[m ..
[33m2a692e0[m[33m ([m[1;31morigin/LMSClient[m[33m)[m configured the client project with react-ts and tailwind css now gonna start work init
[33me0dcf51[m setting up the client project in ts and react
[33m58e8792[m add GetAllCourses Api for teacher. not tested.
[33m55b7277[m MarkAssignment API tested.
[33mce316de[m write marking assignment API, not tested yet.
[33md26a756[m started to write the checkassignment submission API, not completed yet. service layer implementation remaining.
[33m68d6595[m wrote GetAllsubmissions of an Assignment API for teachers
[33m8f587d4[m added some more flags in getAllAssignments for teacher API (just got a realization that need to learn LINQ and EF Core more deeply.)
[33mdb09fe4[m added more flags while returning all the assignments to the students in getallassignment(students) api.
[33md2e9d1b[m trying to add issubmitted and marksscored flags while returining the student assignments in getstudentassignment API
[33m85cac5d[m bug fixed in upload assignment API.
[33m1ae906f[m Upload Assignment service method has a bug(it giving error while checking if the deadline date has been passed or not.)
[33m3bc3046[m bug fixes in upload assignment api.
[33m1e2a380[m some APIs tested and some bug fixes in UploadAssignment API.
[33me80cecd[m logout api done.
[33m669b582[m N-Layered refactor rolled back due to project being broken. will not refactor layers gonna add new functionalities.
[33m0f22ce6[m Architecture refactored failed.
[33m6f41aa5[m Merge branch 'identity' Refactored the whole codebase to use identity framework in my auth APIs.
[33m1c8a275[m preparing to merge the identity branch in the main branch, GetEmployeeById API remaining to fix(issues in EmployeeRepo)
[33me73a1d5[m testing almost done time to merge the identity branch, Identity refactored completed.
[33m343f66f[m Adding More facilitites in Get Apis like course Data in GetTeacher API.
[33m1969dd7[m testing jaari hai.
[33mef08c0c[m tested registerstudent and Employee API. Working currently some samll tests remaining.
[33m97c18b5[m UploadDocuments function has a problem. it only uploads docs for Employees not students so make it generic. transaction problem is solved + chatgpt is shit.
[33m456acc4[m hell bro, debugging the rollback function in AuthRepo still dont know identity uses the same dbcontext or the different one chatgpt is confusing me.
[33m16889f2[m Tested [Authorize] Middleware,was not working but now working
[33m21fddd9[m testing the Refactored Codebase.
[33m965f26a[m starting testing Identity refactored code.
[33mda51a9b[m Refactored Into Identity, Both Employee APIs and Register APIs. Testing remaianing.
[33m8997a6f[m Refactoring the Structure Wants to move Register and Login In AuthServices/AuthRepo. Bhatak gaya tha for a while.
[33mc2ebb2d[m refactor with identity almost done, student login,student registeration remaining and teacher login remaining.
[33m168bf4d[m refactored Add Employee API and refreshAccessToken API with Identity.
[33m705d134[m ...
[33m3062a0b[m ..
[33me652655[m refactored AddEmployee function of Employee Repo, did all the orperations in a single transaction.
[33mf75dc22[m refactoring Manual Auth with Identity Auth.
[33med407ce[m Added SeedData Class For identity for seeding Roles At the Start of the app.
[33m55023f5[m Migrated Identity tables into the DB.
[33m1756321[m deleted the Useless Code for refactoring.
[33mc80214e[m .
[33m860b055[m started with ASP.NET Identity.
[33mf3494de[m Starting to refactor Using ASP.Identity for AUTH Services.
[33m21a9473[m Starting Refactoring Last Commit before Refactoring to save this snapshot of the codebase, Application is working overall.(as of now)
[33m613d8a9[m GetEmployeeById API done.
[33mfdb898c[m changed Database Relation of Employee table with EmployeeDocuments Table to One to One.
[33mb9b05db[m GetEmployeeById API done,Not tested.
[33m078377e[m gonna write GetEmployeebyId and GetStudentById API then I'll start refactoring my codebase By Using Generics.
[33m558a690[m writting GetAllTeachers API.
[33m06f1ff0[m wrote pagination, done , testes, working fine . indutry standard with Pagiantion Class.
[33m59ed431[m GetAllStudentsOfAClass API done with pagination and filtering.
[33m1c91d9c[m starting to write GetAllStudentOfAClass with implementing Pagination. gonna start tommorow.
[33m9cfe521[m wrote GETALLCLASSES API.tested.
[33m46d71ae[m refactored some Routes for GET APIs.
[33m2fc0418[m wrote GetAllClasses API for Admin.
[33m6ab5141[m refactored of assignCourseToClass API completed.
[33m44648b7[m starting to write GET APIs for APP.(Currently starting Refactoring AddCourse API into 2 seperate APIs)
[33m1916937[m get student courses API.
[33m6829b2a[m wrote unit test for class Service Add Class method.
[33m87c9eb9[m wrote 4 unit tests for ClassServiceEnroll Student Method.
[33md1ebaed[m start writting tests Unit/Integration for APIs.
[33m53210b7[m wrote 'getAllassignment of a teacher for a course API.' not tested yet.
[33maf70365[m ??
[33m4caee4b[m wrote Assignment submission API for Assgnment Controller.
[33m7de1ced[m writing postman tests for APIs, further feature adding is paused.
[33m15fa641[m will start writing unit/integration tests for codebase.
[33maf3f05b[m project working fine just like before.
[33mf8dce9d[m project recovery completed after windows crashed.app working 100%, some tests remaining.
[33m0b7e1ca[m dbRecovered finally, after windows crashed.
[33me36b09d[m windows crashed,configuring dotnet in vs code
[33m91ff215[m started working with vs code and ARCH for this project.
[33mfb5c823[m download assignment Api Done. Working but some changes still remaining. like use of Physical File in controller instead of File.
[33mbc15eff[m Upload assignment API debugged working fine.
[33m711047a[m debugging Upload Assignment API.
[33m1e906a4[m writting downloading file API. not completed controller remaining.
[33m42067e5[m Get Student All Due Assignments w.r.t to course API finished.Fetching Assignmet File API not written.
[33m73cb3ee[m writing Student Get Assignment API File Handling Not written yet.
[33m9255e3c[m writing get student assignment API.
[33m7e42d24[m start writing student get assignment API.
[33m05983ae[m upload assignment API finally working.
[33m627e3c9[m memory leak fixed,accidentally used a Controller as a DTO.Swagger exploded.
[33m391c78d[m swagger issue memory issue remains, after opening swagger app eats all the memory of Laptop.
[33m8657318[m ...
[33m35c4f61[m .
[33md845d7c[m starting to debug memory leak.
[33m0178d94[m app not working API request keeps loading and loading.
[33m45346c4[m writing assignment upload API,DB Tables Have issue In relationship between assignment and assignmentsubmission table.
[33mf5097ca[m starting to write upload assignment api.
[33m89af1c7[m write student enrolled in a class Api and bug fix in teacher assigncourse api.
[33m2a77632[m added assigncourse to teacher API. not tested bug fix remaining.
[33mc5ee093[m bug fix in addClass and addCourse APIs
[33mc429f34[m dont know what i am pushing.
[33m01db741[m Added class and course APIs.(AddClass/AddCourse)
[33m5620550[m login Route for student written. not tested, bugs remaining.
[33m85f289a[m studentRegisterApi tested and bug fixed
[33m10f1964[m student registeration Api Completed. testing and bug fixes remaining.
[33m2e73311[m Started Working on Student Register API.Not Done.
[33m826d12b[m reviewed codebase. some minor changes done.
[33ma56b8fe[m refactored completed. our code is now creating data in consistent form in DB(Transactions).JWT Authentication is implemented.
[33m0f86e62[m still refactoring codebase. have to make account and session table relationship one to one currently its one to many.
[33ma5e580d[m refactoring the code to make db consistent by introducing UUIDs through GUID
[33m9c82970[m Authentication and authorization is completed.
[33mb59ad64[m reinstalling Vs Studio.
[33md5fe9a8[m admin authorization and authentication almost done debugging and testing still not completed.
[33m9c13663[m Admin Authorization completed, Debugging Still not done.
[33m98b81fe[m completed login route, now working on middleware and going to change employeeSession and EmployeeAccount relationship to one to one, haven't done it yet.
[33m4513f35[m generated jwt token successfully.
[33mb0cb678[m removed appsettings from git tracked files.
[33mac82bc5[m implementing authentication and authorization,not completed yet,writing login controller for admin.
[33m655a3ad[m initial commit to this repo, scaffold Db done.structured defined and employee register api done.
