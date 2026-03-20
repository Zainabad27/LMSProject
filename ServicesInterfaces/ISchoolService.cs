using LmsApp2.Api.DTOs;

namespace LmsApp2.Api.ServicesInterfaces
{
    public interface ISchoolService
    {
        public Task<Guid> AddSchool(SchoolDto SchoolData);

        public Task<IEnumerable<SendCoursesToFrontendDto>> GetAllCourses(Guid schoolId);

        public Task<IEnumerable<SendTeachersToFrontend>> GetAllTeachers(Guid SchoolId);
    }
}
