using LmsApp2.Api.Services;
using Moq;
using LmsApp2.Api.RepositoriesInterfaces;
using LmsApp2.Api.Models;
using LmsApp2.Api.DTOs;
using System.Threading.Tasks;
using LmsApp2.Api.Exceptions;

namespace LMSPROJECT.Integration_tests;

public class ClassService_UnitTests
{


    [Fact]
    public async Task EnrollStudentSuccessfully()
    {
        Mock<ISchoolRepo> SchoolRepoMock = new Mock<ISchoolRepo>();
        Mock<IClassRepo> ClassRepoMock = new Mock<IClassRepo>();
        Mock<IStudentRepo> StudentRepoMock = new Mock<IStudentRepo>();


        Student std = new Student()
        {
            Studentid = It.IsAny<Guid>(),
            // Studentname = "Test Student",
            Isactive = true,
            Classid = Guid.Empty
        };

        Guid IdThatClassRepoReturns = Guid.NewGuid();
        ClassRepoMock.Setup(crm => crm.GetClass(It.IsAny<Guid>())).ReturnsAsync(IdThatClassRepoReturns);
        StudentRepoMock.Setup(srm => srm.GetStudent(It.IsAny<Guid>())).ReturnsAsync(std);

        Guid ClassId = Guid.NewGuid();
        Guid StdId = Guid.NewGuid();


        EnrollClassDto Edata = new();
        Edata.ClassId = ClassId;
        Edata.StudentId = StdId;

        ClassService Cs = new ClassService(null!, ClassRepoMock.Object, StudentRepoMock.Object);


        var Result = await Cs.EnrollStudent(Edata);

        Assert.Equal(IdThatClassRepoReturns, Result);




    }

    [Fact]
    public async Task StudentDoesNotExistsInSchoolShouldThrowException()
    {
        Mock<ISchoolRepo> SchoolRepoMock = new Mock<ISchoolRepo>();
        Mock<IClassRepo> ClassRepoMock = new Mock<IClassRepo>();
        Mock<IStudentRepo> StudentRepoMock = new Mock<IStudentRepo>();


        Student std = null!;


        Guid IdThatClassRepoReturns = Guid.NewGuid();
        ClassRepoMock.Setup(crm => crm.GetClass(It.IsAny<Guid>())).ReturnsAsync(IdThatClassRepoReturns);
        StudentRepoMock.Setup(srm => srm.GetStudent(It.IsAny<Guid>())).ReturnsAsync(std);

        Guid ClassId = Guid.NewGuid();
        Guid StdId = Guid.NewGuid();


        EnrollClassDto Edata = new();
        Edata.ClassId = ClassId;
        Edata.StudentId = StdId;

        ClassService Cs = new ClassService(null!, ClassRepoMock.Object, StudentRepoMock.Object);


        var ex = await Assert.ThrowsAsync<CustomException>(() =>
          Cs.EnrollStudent(Edata)
       );


        Assert.Equal("this Student does not exists in the database.", ex.Message);
        Assert.Equal(400, ex.StatusCode);



    }



    [Fact]
    public async Task StudentAlreadyEnrolledShouldThrowException()
    {
        Mock<ISchoolRepo> SchoolRepoMock = new Mock<ISchoolRepo>();
        Mock<IClassRepo> ClassRepoMock = new Mock<IClassRepo>();
        Mock<IStudentRepo> StudentRepoMock = new Mock<IStudentRepo>();

        Guid IdThatClassRepoReturns = Guid.NewGuid();

        Guid ClassId = Guid.NewGuid(); // this id is the classid that user want to enroll in but also this is the id student already enrolled in in the returned db model object 
        Guid StdId = Guid.NewGuid();


        Student std = new Student()
        {
            Studentid = It.IsAny<Guid>(),
            // Studentname = "Test Student",
            Isactive = true,
            Classid = ClassId
        };

        ClassRepoMock.Setup(crm => crm.GetClass(It.IsAny<Guid>())).ReturnsAsync(IdThatClassRepoReturns);
        StudentRepoMock.Setup(srm => srm.GetStudent(It.IsAny<Guid>())).ReturnsAsync(std);

        // Guid ClassId = Guid.NewGuid();
        // Guid StdId = Guid.NewGuid();


        EnrollClassDto Edata = new();
        Edata.ClassId = ClassId;
        Edata.StudentId = StdId;

        ClassService Cs = new ClassService(null!, ClassRepoMock.Object, StudentRepoMock.Object);


        var ex = await Assert.ThrowsAsync<CustomException>(() =>
          Cs.EnrollStudent(Edata)
       );


        Assert.Equal("this Student is Already Enrolled in this class.", ex.Message);
        Assert.Equal(400, ex.StatusCode);



    }

    [Fact]
    public async Task StudentNotActiveShouldThrowException()
    {
        Mock<ISchoolRepo> SchoolRepoMock = new Mock<ISchoolRepo>();
        Mock<IClassRepo> ClassRepoMock = new Mock<IClassRepo>();
        Mock<IStudentRepo> StudentRepoMock = new Mock<IStudentRepo>();

        Guid IdThatClassRepoReturns = Guid.NewGuid();

        Guid ClassId = Guid.NewGuid(); // this id is the classid that user want to enroll in but also this is the id student already enrolled in in the returned db model object 
        Guid StdId = Guid.NewGuid();


        Student std = new Student()
        {
            Studentid = It.IsAny<Guid>(),
            // Studentname = "Test Student",
            Isactive = false,
            Classid = Guid.NewGuid()
        };

        ClassRepoMock.Setup(crm => crm.GetClass(It.IsAny<Guid>())).ReturnsAsync(IdThatClassRepoReturns);
        StudentRepoMock.Setup(srm => srm.GetStudent(It.IsAny<Guid>())).ReturnsAsync(std);

        EnrollClassDto Edata = new();
        Edata.ClassId = ClassId;
        Edata.StudentId = StdId;

        ClassService Cs = new ClassService(null!, ClassRepoMock.Object, StudentRepoMock.Object);


        var ex = await Assert.ThrowsAsync<CustomException>(() =>
          Cs.EnrollStudent(Edata)
       );


        Assert.Equal("this student is currently not active.", ex.Message);
        Assert.Equal(400, ex.StatusCode);



    }

    [Fact]
    public async Task EnrollStudentInANonExistentClassShouldThrowException()
    {
        // AAA 
        // Arrange

        Mock<ISchoolRepo> SchoolRepoMock = new Mock<ISchoolRepo>();
        Mock<IClassRepo> ClassRepoMock = new Mock<IClassRepo>();
        Mock<IStudentRepo> StudentRepoMock = new Mock<IStudentRepo>();

        var inputId = Guid.NewGuid();
        var returnId = Guid.NewGuid();


        Student std = new Student()
        {
            Studentid = It.IsAny<Guid>(),
            // Studentname = "Test Student",
            Isactive = true,
            Classid = Guid.Empty
        };


        ClassRepoMock.Setup(cr => cr.GetClass(It.IsAny<Guid>())).ReturnsAsync(Guid.Empty);
        StudentRepoMock.Setup(sr => sr.GetStudent(It.IsAny<Guid>())).ReturnsAsync(std);



        EnrollClassDto EData = new()
        {
            ClassId = It.IsAny<Guid>(),
            StudentId = It.IsAny<Guid>()
        };


        ClassService Cs = new ClassService(null!, ClassRepoMock.Object, StudentRepoMock.Object);

        // action
        var ex = await Assert.ThrowsAsync<CustomException>(() =>
           Cs.EnrollStudent(EData)
        );


        // Assert
        Assert.Equal("This Class Does not Exists in the School.", ex.Message);
        Assert.Equal(400, ex.StatusCode);


    }




    [Fact]

    public async Task AddAClassInASchoolSuccessfully()
    {
        Mock<ISchoolRepo> SchoolRepoMock = new Mock<ISchoolRepo>();
        Mock<IClassRepo> ClassRepoMock = new Mock<IClassRepo>();

        Guid SchoolId = Guid.NewGuid();
        SchoolRepoMock.Setup(srm => srm.GetSchoolByName("Some Valid SchoolName")).ReturnsAsync(SchoolId);
        ClassRepoMock.Setup(crm => crm.GetClass(SchoolId, "Valid Section", "Valid Grade")).ReturnsAsync(Guid.Empty);
        ClassDto cls = new()
        {
            SchoolName = "Some Valid SchoolName",
            ClassGrade = "Valid Grade",
            ClassSection = "Valid Section"

        };
        Guid ClsId = It.IsAny<Guid>();
        ClassRepoMock.Setup(crm => crm.AddClass(cls, SchoolId)).ReturnsAsync(ClsId);

        ClassService Cs = new(SchoolRepoMock.Object, ClassRepoMock.Object, null!);

        var Result = await Cs.AddClass(cls);



        Assert.Equal(ClsId, Result);





    }
}