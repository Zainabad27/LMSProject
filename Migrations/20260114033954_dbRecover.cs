using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LmsApp2.Api.Migrations
{
    /// <inheritdoc />
    public partial class dbRecover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "school",
                columns: table => new
                {
                    schoolid = table.Column<Guid>(type: "uuid", nullable: false),
                    schoolname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("school_pkey", x => x.schoolid);
                });

            migrationBuilder.CreateTable(
                name: "class",
                columns: table => new
                {
                    classid = table.Column<Guid>(type: "uuid", nullable: false),
                    classgrade = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    classsection = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    schoolid = table.Column<Guid>(type: "uuid", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("class_pkey", x => x.classid);
                    table.ForeignKey(
                        name: "class_schoolid_fkey",
                        column: x => x.schoolid,
                        principalTable: "school",
                        principalColumn: "schoolid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    employeeid = table.Column<Guid>(type: "uuid", nullable: false),
                    schoolid = table.Column<Guid>(type: "uuid", nullable: true),
                    employeename = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    employeedesignation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    contact = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    religion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    nationality = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("employees_pkey", x => x.employeeid);
                    table.ForeignKey(
                        name: "employees_schoolid_fkey",
                        column: x => x.schoolid,
                        principalTable: "school",
                        principalColumn: "schoolid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    studentid = table.Column<Guid>(type: "uuid", nullable: false),
                    studentname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    guardianname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    guardiancontact = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    bloodgroup = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    religion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    nationality = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    schoolid = table.Column<Guid>(type: "uuid", nullable: true),
                    classid = table.Column<Guid>(type: "uuid", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    birthdate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("students_pkey", x => x.studentid);
                    table.ForeignKey(
                        name: "students_classid_fkey",
                        column: x => x.classid,
                        principalTable: "class",
                        principalColumn: "classid",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "students_schoolid_fkey",
                        column: x => x.schoolid,
                        principalTable: "school",
                        principalColumn: "schoolid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    courseid = table.Column<Guid>(type: "uuid", nullable: false),
                    boardordepartment = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CourseName = table.Column<string>(type: "character varying(130)", maxLength: 130, nullable: false),
                    Class = table.Column<Guid>(type: "uuid", nullable: true),
                    Teacher = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("course_pkey", x => x.courseid);
                    table.ForeignKey(
                        name: "course_Class_fkey",
                        column: x => x.Class,
                        principalTable: "class",
                        principalColumn: "classid");
                    table.ForeignKey(
                        name: "course_Teacher_fkey",
                        column: x => x.Teacher,
                        principalTable: "employees",
                        principalColumn: "employeeid");
                });

            migrationBuilder.CreateTable(
                name: "employeeaccountinfo",
                columns: table => new
                {
                    accountid = table.Column<Guid>(type: "uuid", nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    employeeid = table.Column<Guid>(type: "uuid", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("employeeaccountinfo_pkey", x => x.accountid);
                    table.ForeignKey(
                        name: "employeeaccountinfo_employeeid_fkey",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "employeeid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employeeadditionaldocs",
                columns: table => new
                {
                    documentid = table.Column<Guid>(type: "uuid", nullable: false),
                    employeeid = table.Column<Guid>(type: "uuid", nullable: true),
                    documentpath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    documenttype = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("employeeadditionaldocs_pkey", x => x.documentid);
                    table.ForeignKey(
                        name: "employeeadditionaldocs_employeeid_fkey",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "employeeid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employeeattendance",
                columns: table => new
                {
                    attendanceid = table.Column<Guid>(type: "uuid", nullable: false),
                    employeeid = table.Column<Guid>(type: "uuid", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("employeeattendance_pkey", x => x.attendanceid);
                    table.ForeignKey(
                        name: "employeeattendance_employeeid_fkey",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "employeeid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employeedocuments",
                columns: table => new
                {
                    documentid = table.Column<Guid>(type: "uuid", nullable: false),
                    employeeid = table.Column<Guid>(type: "uuid", nullable: true),
                    cnicfront = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    cnicback = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    photo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("employeedocuments_pkey", x => x.documentid);
                    table.ForeignKey(
                        name: "employeedocuments_employeeid_fkey",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "employeeid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employeefinances",
                columns: table => new
                {
                    financeid = table.Column<Guid>(type: "uuid", nullable: false),
                    employeeid = table.Column<Guid>(type: "uuid", nullable: true),
                    month = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    basicsalary = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    allowances = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, defaultValueSql: "0"),
                    overtimepay = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, defaultValueSql: "0"),
                    bonuses = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, defaultValueSql: "0"),
                    taxdeduction = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, defaultValueSql: "0"),
                    dueonemployee = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, defaultValueSql: "0"),
                    otherdeductions = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, defaultValueSql: "0"),
                    otherdeductionsreasons = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    paiddate = table.Column<DateOnly>(type: "date", nullable: false),
                    paidmethod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("employeefinances_pkey", x => x.financeid);
                    table.ForeignKey(
                        name: "employeefinances_employeeid_fkey",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "employeeid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exam",
                columns: table => new
                {
                    examid = table.Column<Guid>(type: "uuid", nullable: false),
                    examdate = table.Column<DateOnly>(type: "date", nullable: false),
                    totalmarks = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: true),
                    subject = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    examtype = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    teacherid = table.Column<Guid>(type: "uuid", nullable: true),
                    classid = table.Column<Guid>(type: "uuid", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("exam_pkey", x => x.examid);
                    table.ForeignKey(
                        name: "exam_classid_fkey",
                        column: x => x.classid,
                        principalTable: "class",
                        principalColumn: "classid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "exam_teacherid_fkey",
                        column: x => x.teacherid,
                        principalTable: "employees",
                        principalColumn: "employeeid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "studentaccountinfo",
                columns: table => new
                {
                    accountid = table.Column<Guid>(type: "uuid", nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    studentid = table.Column<Guid>(type: "uuid", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("studentaccountinfo_pkey", x => x.accountid);
                    table.ForeignKey(
                        name: "studentaccountinfo_studentid_fkey",
                        column: x => x.studentid,
                        principalTable: "students",
                        principalColumn: "studentid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "studentadditionaldocs",
                columns: table => new
                {
                    documentid = table.Column<Guid>(type: "uuid", nullable: false),
                    studentid = table.Column<Guid>(type: "uuid", nullable: true),
                    documentpath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    documenttype = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("studentadditionaldocs_pkey", x => x.documentid);
                    table.ForeignKey(
                        name: "studentadditionaldocs_studentid_fkey",
                        column: x => x.studentid,
                        principalTable: "students",
                        principalColumn: "studentid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "studentattendance",
                columns: table => new
                {
                    attendanceid = table.Column<Guid>(type: "uuid", nullable: false),
                    studentid = table.Column<Guid>(type: "uuid", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("studentattendance_pkey", x => x.attendanceid);
                    table.ForeignKey(
                        name: "studentattendance_studentid_fkey",
                        column: x => x.studentid,
                        principalTable: "students",
                        principalColumn: "studentid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "studentdocuments",
                columns: table => new
                {
                    documentid = table.Column<Guid>(type: "uuid", nullable: false),
                    studentid = table.Column<Guid>(type: "uuid", nullable: true),
                    cnicfrontorbform = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cnicbackorbform = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    photo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("studentdocuments_pkey", x => x.documentid);
                    table.ForeignKey(
                        name: "studentdocuments_studentid_fkey",
                        column: x => x.studentid,
                        principalTable: "students",
                        principalColumn: "studentid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "studentfinances",
                columns: table => new
                {
                    financeid = table.Column<Guid>(type: "uuid", nullable: false),
                    studentid = table.Column<Guid>(type: "uuid", nullable: true),
                    issuedby = table.Column<Guid>(type: "uuid", nullable: true),
                    tuitionfee = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, defaultValueSql: "0"),
                    examinationfee = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, defaultValueSql: "0"),
                    coursefee = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, defaultValueSql: "0"),
                    transportfee = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, defaultValueSql: "0"),
                    othercharges = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, defaultValueSql: "0"),
                    otherchargesreasons = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    month = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    issueddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    amountpaid = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, defaultValueSql: "0"),
                    amountremaining = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, defaultValueSql: "0"),
                    paidmethod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    remarks = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    paiddate = table.Column<DateOnly>(type: "date", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("studentfinances_pkey", x => x.financeid);
                    table.ForeignKey(
                        name: "studentfinances_issuedby_fkey",
                        column: x => x.issuedby,
                        principalTable: "employees",
                        principalColumn: "employeeid",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "studentfinances_studentid_fkey",
                        column: x => x.studentid,
                        principalTable: "students",
                        principalColumn: "studentid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assignments",
                columns: table => new
                {
                    assignmentid = table.Column<Guid>(type: "uuid", nullable: false),
                    employeeid = table.Column<Guid>(type: "uuid", nullable: true),
                    classid = table.Column<Guid>(type: "uuid", nullable: true),
                    deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    totalmarks = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    coursename = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    courseid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("assignments_pkey", x => x.assignmentid);
                    table.ForeignKey(
                        name: "assignments_classid_fkey",
                        column: x => x.classid,
                        principalTable: "class",
                        principalColumn: "classid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "assignments_courseid_fkey",
                        column: x => x.courseid,
                        principalTable: "course",
                        principalColumn: "courseid");
                    table.ForeignKey(
                        name: "assignments_employeeid_fkey",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "employeeid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "employeesessions",
                columns: table => new
                {
                    sessionid = table.Column<Guid>(type: "uuid", nullable: false),
                    refreshtoken = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    expiresat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    employeeaccountid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("employeesessions_pkey", x => x.sessionid);
                    table.ForeignKey(
                        name: "employeesessions_employeeaccountid_fkey",
                        column: x => x.employeeaccountid,
                        principalTable: "employeeaccountinfo",
                        principalColumn: "accountid");
                });

            migrationBuilder.CreateTable(
                name: "examcontent",
                columns: table => new
                {
                    examcontentid = table.Column<Guid>(type: "uuid", nullable: false),
                    contentpath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    examid = table.Column<Guid>(type: "uuid", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("examcontent_pkey", x => x.examcontentid);
                    table.ForeignKey(
                        name: "examcontent_examid_fkey",
                        column: x => x.examid,
                        principalTable: "exam",
                        principalColumn: "examid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "examenrollments",
                columns: table => new
                {
                    enrollmentid = table.Column<Guid>(type: "uuid", nullable: false),
                    examid = table.Column<Guid>(type: "uuid", nullable: true),
                    studentid = table.Column<Guid>(type: "uuid", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("examenrollments_pkey", x => x.enrollmentid);
                    table.ForeignKey(
                        name: "examenrollments_examid_fkey",
                        column: x => x.examid,
                        principalTable: "exam",
                        principalColumn: "examid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "examenrollments_studentid_fkey",
                        column: x => x.studentid,
                        principalTable: "students",
                        principalColumn: "studentid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "studentsessions",
                columns: table => new
                {
                    sessionid = table.Column<Guid>(type: "uuid", nullable: false),
                    studentaccountid = table.Column<Guid>(type: "uuid", nullable: true),
                    refreshtoken = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    expiresat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("studentsessions_pkey", x => x.sessionid);
                    table.ForeignKey(
                        name: "studentsessions_studentaccountid_fkey",
                        column: x => x.studentaccountid,
                        principalTable: "studentaccountinfo",
                        principalColumn: "accountid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assignmentquestion",
                columns: table => new
                {
                    assignmentquestionid = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    assignmentid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("assignmentquestion_pkey", x => x.assignmentquestionid);
                    table.ForeignKey(
                        name: "assignmentquestion_assignmentid_fkey",
                        column: x => x.assignmentid,
                        principalTable: "assignments",
                        principalColumn: "assignmentid");
                });

            migrationBuilder.CreateTable(
                name: "assignmentsubmission",
                columns: table => new
                {
                    assignmentsubmissionid = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    marksscored = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: true),
                    studentid = table.Column<Guid>(type: "uuid", nullable: true),
                    assignmentid = table.Column<Guid>(type: "uuid", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("assignmentsubmission_pkey", x => x.assignmentsubmissionid);
                    table.ForeignKey(
                        name: "assignmentsubmission_assignmentid_fkey",
                        column: x => x.assignmentid,
                        principalTable: "assignments",
                        principalColumn: "assignmentid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "assignmentsubmission_studentid_fkey",
                        column: x => x.studentid,
                        principalTable: "students",
                        principalColumn: "studentid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "examresult",
                columns: table => new
                {
                    resultid = table.Column<Guid>(type: "uuid", nullable: false),
                    marksscored = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: true),
                    enrollmentid = table.Column<Guid>(type: "uuid", nullable: true),
                    studentid = table.Column<Guid>(type: "uuid", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("examresult_pkey", x => x.resultid);
                    table.ForeignKey(
                        name: "examresult_enrollmentid_fkey",
                        column: x => x.enrollmentid,
                        principalTable: "examenrollments",
                        principalColumn: "enrollmentid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "examresult_studentid_fkey",
                        column: x => x.studentid,
                        principalTable: "students",
                        principalColumn: "studentid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "assignmentquestion_assignmentid_key",
                table: "assignmentquestion",
                column: "assignmentid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assignments_classid",
                table: "assignments",
                column: "classid");

            migrationBuilder.CreateIndex(
                name: "IX_assignments_courseid",
                table: "assignments",
                column: "courseid");

            migrationBuilder.CreateIndex(
                name: "IX_assignments_employeeid",
                table: "assignments",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "IX_assignmentsubmission_assignmentid",
                table: "assignmentsubmission",
                column: "assignmentid");

            migrationBuilder.CreateIndex(
                name: "IX_assignmentsubmission_studentid",
                table: "assignmentsubmission",
                column: "studentid");

            migrationBuilder.CreateIndex(
                name: "IX_class_schoolid",
                table: "class",
                column: "schoolid");

            migrationBuilder.CreateIndex(
                name: "IX_course_Class",
                table: "course",
                column: "Class");

            migrationBuilder.CreateIndex(
                name: "IX_course_Teacher",
                table: "course",
                column: "Teacher");

            migrationBuilder.CreateIndex(
                name: "employeeaccountinfo_email_key",
                table: "employeeaccountinfo",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "employeeaccountinfo_employeeid_key",
                table: "employeeaccountinfo",
                column: "employeeid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employeeadditionaldocs_employeeid",
                table: "employeeadditionaldocs",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "IX_employeeattendance_employeeid",
                table: "employeeattendance",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "IX_employeedocuments_employeeid",
                table: "employeedocuments",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "IX_employeefinances_employeeid",
                table: "employeefinances",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "employees_contact_key",
                table: "employees",
                column: "contact",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employees_schoolid",
                table: "employees",
                column: "schoolid");

            migrationBuilder.CreateIndex(
                name: "employeesessions_employeeaccountid_key",
                table: "employeesessions",
                column: "employeeaccountid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_exam_classid",
                table: "exam",
                column: "classid");

            migrationBuilder.CreateIndex(
                name: "IX_exam_teacherid",
                table: "exam",
                column: "teacherid");

            migrationBuilder.CreateIndex(
                name: "IX_examcontent_examid",
                table: "examcontent",
                column: "examid");

            migrationBuilder.CreateIndex(
                name: "IX_examenrollments_examid",
                table: "examenrollments",
                column: "examid");

            migrationBuilder.CreateIndex(
                name: "IX_examenrollments_studentid",
                table: "examenrollments",
                column: "studentid");

            migrationBuilder.CreateIndex(
                name: "examresult_enrollmentid_key",
                table: "examresult",
                column: "enrollmentid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_examresult_studentid",
                table: "examresult",
                column: "studentid");

            migrationBuilder.CreateIndex(
                name: "school_schoolname_key",
                table: "school",
                column: "schoolname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "studentaccountinfo_email_key",
                table: "studentaccountinfo",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "studentaccountinfo_studentid_key",
                table: "studentaccountinfo",
                column: "studentid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_studentadditionaldocs_studentid",
                table: "studentadditionaldocs",
                column: "studentid");

            migrationBuilder.CreateIndex(
                name: "IX_studentattendance_studentid",
                table: "studentattendance",
                column: "studentid");

            migrationBuilder.CreateIndex(
                name: "studentdocuments_studentid_key",
                table: "studentdocuments",
                column: "studentid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_studentfinances_issuedby",
                table: "studentfinances",
                column: "issuedby");

            migrationBuilder.CreateIndex(
                name: "IX_studentfinances_studentid",
                table: "studentfinances",
                column: "studentid");

            migrationBuilder.CreateIndex(
                name: "IX_students_classid",
                table: "students",
                column: "classid");

            migrationBuilder.CreateIndex(
                name: "IX_students_schoolid",
                table: "students",
                column: "schoolid");

            migrationBuilder.CreateIndex(
                name: "IX_studentsessions_studentaccountid",
                table: "studentsessions",
                column: "studentaccountid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assignmentquestion");

            migrationBuilder.DropTable(
                name: "assignmentsubmission");

            migrationBuilder.DropTable(
                name: "employeeadditionaldocs");

            migrationBuilder.DropTable(
                name: "employeeattendance");

            migrationBuilder.DropTable(
                name: "employeedocuments");

            migrationBuilder.DropTable(
                name: "employeefinances");

            migrationBuilder.DropTable(
                name: "employeesessions");

            migrationBuilder.DropTable(
                name: "examcontent");

            migrationBuilder.DropTable(
                name: "examresult");

            migrationBuilder.DropTable(
                name: "studentadditionaldocs");

            migrationBuilder.DropTable(
                name: "studentattendance");

            migrationBuilder.DropTable(
                name: "studentdocuments");

            migrationBuilder.DropTable(
                name: "studentfinances");

            migrationBuilder.DropTable(
                name: "studentsessions");

            migrationBuilder.DropTable(
                name: "assignments");

            migrationBuilder.DropTable(
                name: "employeeaccountinfo");

            migrationBuilder.DropTable(
                name: "examenrollments");

            migrationBuilder.DropTable(
                name: "studentaccountinfo");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "exam");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "class");

            migrationBuilder.DropTable(
                name: "school");
        }
    }
}
