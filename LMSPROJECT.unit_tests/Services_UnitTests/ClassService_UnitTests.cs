using LmsApp2.Api.Services;
using Moq;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.Models;
using LmsApp2.Api.DTOs;
using System.Threading.Tasks;

namespace LMSPROJECT.Integration_tests;

public class ClassService_UnitTests
{
    [Fact]
    public async Task EnrollStudent_FuncUnitTests()
    {
        // AAA 
        // Arrange
        var SchoolRepoMock = new Mock<ISchoolRepo>();
        var ClassRepoMock = new Mock<IClassRepo>();
        var StudentRepoMock = new Mock<IStudentRepo>();
        var inputId = Guid.NewGuid();
        var returnId = Guid.NewGuid();


        Student std=new Student()
        {
            Studentid = inputId,
            Studentname = "Test Student",
            Isactive = true,
            Classid = Guid.Empty
        };


        ClassRepoMock.Setup(cr => cr.GetClass(It.IsAny<Guid>())).ReturnsAsync(returnId);
        StudentRepoMock.Setup(sr=>sr.GetStudent(It.IsAny<Guid>())).ReturnsAsync(std);



        EnrollClassDto EData=new();
        EData.ClassId=It.IsAny<Guid>(); 
        EData.StudentId=It.IsAny<Guid>();   


        ClassService Cs = new ClassService(null, ClassRepoMock.Object, StudentRepoMock.Object);

    // action
       Guid ClassID= await Cs.EnrollStudent(EData);


        // Assert
        Assert.Equal(returnId, ClassID);


    }
}