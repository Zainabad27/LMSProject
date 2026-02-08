using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LmsApp2.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    EmployeeName = table.Column<string>(type: "text", nullable: true),
                    Employeedesignation = table.Column<string>(type: "text", nullable: true),
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
                    StudentName = table.Column<string>(type: "text", nullable: true),
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId_InMainTable = table.Column<Guid>(type: "uuid", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    TokenExpiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Employeeid = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_employees_Employeeid",
                        column: x => x.Employeeid,
                        principalTable: "employees",
                        principalColumn: "employeeid");
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Employeeid",
                table: "AspNetUsers",
                column: "Employeeid");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

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
                column: "employeeid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employeefinances_employeeid",
                table: "employeefinances",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "IX_employees_schoolid",
                table: "employees",
                column: "schoolid");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

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
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "assignments");

            migrationBuilder.DropTable(
                name: "examenrollments");

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
