using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LmsApp2.Api.Models;

public partial class LmsDatabaseContext : DbContext
{
    public LmsDatabaseContext()
    {
    }

    public LmsDatabaseContext(DbContextOptions<LmsDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Assignmentquestion> Assignmentquestions { get; set; }

    public virtual DbSet<Assignmentsubmission> Assignmentsubmissions { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Employeeaccountinfo> Employeeaccountinfos { get; set; }

    public virtual DbSet<Employeeadditionaldoc> Employeeadditionaldocs { get; set; }

    public virtual DbSet<Employeeattendance> Employeeattendances { get; set; }

    public virtual DbSet<Employeedocument> Employeedocuments { get; set; }

    public virtual DbSet<Employeefinance> Employeefinances { get; set; }

    public virtual DbSet<Employeesession> Employeesessions { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Examcontent> Examcontents { get; set; }

    public virtual DbSet<Examenrollment> Examenrollments { get; set; }

    public virtual DbSet<Examresult> Examresults { get; set; }

    public virtual DbSet<School> Schools { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Studentaccountinfo> Studentaccountinfos { get; set; }

    public virtual DbSet<Studentadditionaldoc> Studentadditionaldocs { get; set; }

    public virtual DbSet<Studentattendance> Studentattendances { get; set; }

    public virtual DbSet<Studentdocument> Studentdocuments { get; set; }

    public virtual DbSet<Studentfinance> Studentfinances { get; set; }

    public virtual DbSet<Studentsession> Studentsessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=LMS_Database;Username=postgres;Password=27135789");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Assignmentid).HasName("assignments_pkey");

            entity.ToTable("assignments");

            entity.Property(e => e.Assignmentid)
                .ValueGeneratedNever()
                .HasColumnName("assignmentid");
            entity.Property(e => e.Classid).HasColumnName("classid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Deadline).HasColumnName("deadline");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Totalmarks)
                .HasPrecision(6, 2)
                .HasColumnName("totalmarks");

            entity.HasOne(d => d.Class).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.Classid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("assignments_classid_fkey");

            entity.HasOne(d => d.Employee).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.Employeeid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("assignments_employeeid_fkey");
        });

        modelBuilder.Entity<Assignmentquestion>(entity =>
        {
            entity.HasKey(e => e.Assignmentquestionid).HasName("assignmentquestion_pkey");

            entity.ToTable("assignmentquestion");

            entity.Property(e => e.Assignmentquestionid)
                .ValueGeneratedNever()
                .HasColumnName("assignmentquestionid");
            entity.Property(e => e.Assignmentid).HasColumnName("assignmentid");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");

            entity.HasOne(d => d.Assignment).WithMany(p => p.Assignmentquestions)
                .HasForeignKey(d => d.Assignmentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("assignmentquestion_assignmentid_fkey");

            entity.HasOne(d => d.Employee).WithMany(p => p.Assignmentquestions)
                .HasForeignKey(d => d.Employeeid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("assignmentquestion_employeeid_fkey");
        });

        modelBuilder.Entity<Assignmentsubmission>(entity =>
        {
            entity.HasKey(e => e.Assignmentsubmissionid).HasName("assignmentsubmission_pkey");

            entity.ToTable("assignmentsubmission");

            entity.Property(e => e.Assignmentsubmissionid)
                .ValueGeneratedNever()
                .HasColumnName("assignmentsubmissionid");
            entity.Property(e => e.Assignmentid).HasColumnName("assignmentid");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Marksscored)
                .HasPrecision(6, 2)
                .HasColumnName("marksscored");
            entity.Property(e => e.Studentid).HasColumnName("studentid");

            entity.HasOne(d => d.Assignment).WithMany(p => p.Assignmentsubmissions)
                .HasForeignKey(d => d.Assignmentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("assignmentsubmission_assignmentid_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.Assignmentsubmissions)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("assignmentsubmission_studentid_fkey");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Classid).HasName("class_pkey");

            entity.ToTable("class");

            entity.Property(e => e.Classid)
                .ValueGeneratedNever()
                .HasColumnName("classid");
            entity.Property(e => e.Classgrade)
                .HasMaxLength(50)
                .HasColumnName("classgrade");
            entity.Property(e => e.Classsection)
                .HasMaxLength(10)
                .HasColumnName("classsection");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Schoolid).HasColumnName("schoolid");

            entity.HasOne(d => d.School).WithMany(p => p.Classes)
                .HasForeignKey(d => d.Schoolid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("class_schoolid_fkey");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Courseid).HasName("course_pkey");

            entity.ToTable("course");

            entity.Property(e => e.Courseid)
                .ValueGeneratedNever()
                .HasColumnName("courseid");
            entity.Property(e => e.Boardordepartment)
                .HasMaxLength(100)
                .HasColumnName("boardordepartment");
            entity.Property(e => e.CourseName).HasMaxLength(130);
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");

            entity.HasOne(d => d.ClassNavigation).WithMany(p => p.Courses)
                .HasForeignKey(d => d.Class)
                .HasConstraintName("course_Class_fkey");

            entity.HasOne(d => d.TeacherNavigation).WithMany(p => p.Courses)
                .HasForeignKey(d => d.Teacher)
                .HasConstraintName("course_Teacher_fkey");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Employeeid).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.HasIndex(e => e.Contact, "employees_contact_key").IsUnique();

            entity.Property(e => e.Employeeid)
                .ValueGeneratedNever()
                .HasColumnName("employeeid");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Contact)
                .HasMaxLength(20)
                .HasColumnName("contact");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Employeedesignation)
                .HasMaxLength(100)
                .HasColumnName("employeedesignation");
            entity.Property(e => e.Employeename)
                .HasMaxLength(100)
                .HasColumnName("employeename");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Nationality)
                .HasMaxLength(50)
                .HasColumnName("nationality");
            entity.Property(e => e.Religion)
                .HasMaxLength(50)
                .HasColumnName("religion");
            entity.Property(e => e.Schoolid).HasColumnName("schoolid");

            entity.HasOne(d => d.School).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Schoolid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("employees_schoolid_fkey");
        });

        modelBuilder.Entity<Employeeaccountinfo>(entity =>
        {
            entity.HasKey(e => e.Accountid).HasName("employeeaccountinfo_pkey");

            entity.ToTable("employeeaccountinfo");

            entity.HasIndex(e => e.Email, "employeeaccountinfo_email_key").IsUnique();

            entity.HasIndex(e => e.Employeeid, "employeeaccountinfo_employeeid_key").IsUnique();

            entity.Property(e => e.Accountid)
                .ValueGeneratedNever()
                .HasColumnName("accountid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");

            entity.HasOne(d => d.Employee).WithOne(p => p.Employeeaccountinfo)
                .HasForeignKey<Employeeaccountinfo>(d => d.Employeeid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("employeeaccountinfo_employeeid_fkey");
        });

        modelBuilder.Entity<Employeeadditionaldoc>(entity =>
        {
            entity.HasKey(e => e.Documentid).HasName("employeeadditionaldocs_pkey");

            entity.ToTable("employeeadditionaldocs");

            entity.Property(e => e.Documentid)
                .ValueGeneratedNever()
                .HasColumnName("documentid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Documentpath)
                .HasMaxLength(255)
                .HasColumnName("documentpath");
            entity.Property(e => e.Documenttype)
                .HasMaxLength(100)
                .HasColumnName("documenttype");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");

            entity.HasOne(d => d.Employee).WithMany(p => p.Employeeadditionaldocs)
                .HasForeignKey(d => d.Employeeid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("employeeadditionaldocs_employeeid_fkey");
        });

        modelBuilder.Entity<Employeeattendance>(entity =>
        {
            entity.HasKey(e => e.Attendanceid).HasName("employeeattendance_pkey");

            entity.ToTable("employeeattendance");

            entity.Property(e => e.Attendanceid)
                .ValueGeneratedNever()
                .HasColumnName("attendanceid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");

            entity.HasOne(d => d.Employee).WithMany(p => p.Employeeattendances)
                .HasForeignKey(d => d.Employeeid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("employeeattendance_employeeid_fkey");
        });

        modelBuilder.Entity<Employeedocument>(entity =>
        {
            entity.HasKey(e => e.Documentid).HasName("employeedocuments_pkey");

            entity.ToTable("employeedocuments");

            entity.Property(e => e.Documentid)
                .ValueGeneratedNever()
                .HasColumnName("documentid");
            entity.Property(e => e.Cnicback)
                .HasMaxLength(255)
                .HasColumnName("cnicback");
            entity.Property(e => e.Cnicfront)
                .HasMaxLength(255)
                .HasColumnName("cnicfront");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .HasColumnName("photo");

            entity.HasOne(d => d.Employee).WithMany(p => p.Employeedocuments)
                .HasForeignKey(d => d.Employeeid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("employeedocuments_employeeid_fkey");
        });

        modelBuilder.Entity<Employeefinance>(entity =>
        {
            entity.HasKey(e => e.Financeid).HasName("employeefinances_pkey");

            entity.ToTable("employeefinances");

            entity.Property(e => e.Financeid)
                .ValueGeneratedNever()
                .HasColumnName("financeid");
            entity.Property(e => e.Allowances)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("allowances");
            entity.Property(e => e.Basicsalary)
                .HasPrecision(12, 2)
                .HasColumnName("basicsalary");
            entity.Property(e => e.Bonuses)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("bonuses");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Dueonemployee)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("dueonemployee");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Month)
                .HasMaxLength(20)
                .HasColumnName("month");
            entity.Property(e => e.Otherdeductions)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("otherdeductions");
            entity.Property(e => e.Otherdeductionsreasons)
                .HasMaxLength(255)
                .HasColumnName("otherdeductionsreasons");
            entity.Property(e => e.Overtimepay)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("overtimepay");
            entity.Property(e => e.Paiddate).HasColumnName("paiddate");
            entity.Property(e => e.Paidmethod)
                .HasMaxLength(50)
                .HasColumnName("paidmethod");
            entity.Property(e => e.Taxdeduction)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("taxdeduction");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Employee).WithMany(p => p.Employeefinances)
                .HasForeignKey(d => d.Employeeid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("employeefinances_employeeid_fkey");
        });

        modelBuilder.Entity<Employeesession>(entity =>
        {
            entity.HasKey(e => e.Sessionid).HasName("employeesessions_pkey");

            entity.ToTable("employeesessions");

            entity.HasIndex(e => e.Employeeaccountid, "employeesessions_employeeaccountid_key").IsUnique();

            entity.Property(e => e.Sessionid)
                .ValueGeneratedNever()
                .HasColumnName("sessionid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Employeeaccountid).HasColumnName("employeeaccountid");
            entity.Property(e => e.Expiresat).HasColumnName("expiresat");
            entity.Property(e => e.Refreshtoken)
                .HasMaxLength(255)
                .HasColumnName("refreshtoken");

            entity.HasOne(d => d.Employeeaccount).WithOne(p => p.Employeesession)
                .HasForeignKey<Employeesession>(d => d.Employeeaccountid)
                .HasConstraintName("employeesessions_employeeaccountid_fkey");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.Examid).HasName("exam_pkey");

            entity.ToTable("exam");

            entity.Property(e => e.Examid)
                .ValueGeneratedNever()
                .HasColumnName("examid");
            entity.Property(e => e.Classid).HasColumnName("classid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Examdate).HasColumnName("examdate");
            entity.Property(e => e.Examtype)
                .HasMaxLength(50)
                .HasColumnName("examtype");
            entity.Property(e => e.Subject)
                .HasMaxLength(100)
                .HasColumnName("subject");
            entity.Property(e => e.Teacherid).HasColumnName("teacherid");
            entity.Property(e => e.Totalmarks)
                .HasPrecision(6, 2)
                .HasColumnName("totalmarks");

            entity.HasOne(d => d.Class).WithMany(p => p.Exams)
                .HasForeignKey(d => d.Classid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("exam_classid_fkey");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Exams)
                .HasForeignKey(d => d.Teacherid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("exam_teacherid_fkey");
        });

        modelBuilder.Entity<Examcontent>(entity =>
        {
            entity.HasKey(e => e.Examcontentid).HasName("examcontent_pkey");

            entity.ToTable("examcontent");

            entity.Property(e => e.Examcontentid)
                .ValueGeneratedNever()
                .HasColumnName("examcontentid");
            entity.Property(e => e.Contentpath)
                .HasMaxLength(255)
                .HasColumnName("contentpath");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Examid).HasColumnName("examid");

            entity.HasOne(d => d.Exam).WithMany(p => p.Examcontents)
                .HasForeignKey(d => d.Examid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("examcontent_examid_fkey");
        });

        modelBuilder.Entity<Examenrollment>(entity =>
        {
            entity.HasKey(e => e.Enrollmentid).HasName("examenrollments_pkey");

            entity.ToTable("examenrollments");

            entity.Property(e => e.Enrollmentid)
                .ValueGeneratedNever()
                .HasColumnName("enrollmentid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Examid).HasColumnName("examid");
            entity.Property(e => e.Studentid).HasColumnName("studentid");

            entity.HasOne(d => d.Exam).WithMany(p => p.Examenrollments)
                .HasForeignKey(d => d.Examid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("examenrollments_examid_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.Examenrollments)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("examenrollments_studentid_fkey");
        });

        modelBuilder.Entity<Examresult>(entity =>
        {
            entity.HasKey(e => e.Resultid).HasName("examresult_pkey");

            entity.ToTable("examresult");

            entity.HasIndex(e => e.Enrollmentid, "examresult_enrollmentid_key").IsUnique();

            entity.Property(e => e.Resultid)
                .ValueGeneratedNever()
                .HasColumnName("resultid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Enrollmentid).HasColumnName("enrollmentid");
            entity.Property(e => e.Marksscored)
                .HasPrecision(6, 2)
                .HasColumnName("marksscored");
            entity.Property(e => e.Studentid).HasColumnName("studentid");

            entity.HasOne(d => d.Enrollment).WithOne(p => p.Examresult)
                .HasForeignKey<Examresult>(d => d.Enrollmentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("examresult_enrollmentid_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.Examresults)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("examresult_studentid_fkey");
        });

        modelBuilder.Entity<School>(entity =>
        {
            entity.HasKey(e => e.Schoolid).HasName("school_pkey");

            entity.ToTable("school");

            entity.HasIndex(e => e.Schoolname, "school_schoolname_key").IsUnique();

            entity.Property(e => e.Schoolid)
                .ValueGeneratedNever()
                .HasColumnName("schoolid");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Schoolname)
                .HasMaxLength(255)
                .HasColumnName("schoolname");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Studentid).HasName("students_pkey");

            entity.ToTable("students");

            entity.Property(e => e.Studentid)
                .ValueGeneratedNever()
                .HasColumnName("studentid");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Bloodgroup)
                .HasMaxLength(10)
                .HasColumnName("bloodgroup");
            entity.Property(e => e.Classid).HasColumnName("classid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Guardiancontact)
                .HasMaxLength(20)
                .HasColumnName("guardiancontact");
            entity.Property(e => e.Guardianname)
                .HasMaxLength(100)
                .HasColumnName("guardianname");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Nationality)
                .HasMaxLength(50)
                .HasColumnName("nationality");
            entity.Property(e => e.Religion)
                .HasMaxLength(50)
                .HasColumnName("religion");
            entity.Property(e => e.Schoolid).HasColumnName("schoolid");
            entity.Property(e => e.Studentname)
                .HasMaxLength(100)
                .HasColumnName("studentname");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.Classid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("students_classid_fkey");

            entity.HasOne(d => d.School).WithMany(p => p.Students)
                .HasForeignKey(d => d.Schoolid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("students_schoolid_fkey");
        });

        modelBuilder.Entity<Studentaccountinfo>(entity =>
        {
            entity.HasKey(e => e.Accountid).HasName("studentaccountinfo_pkey");

            entity.ToTable("studentaccountinfo");

            entity.HasIndex(e => e.Email, "studentaccountinfo_email_key").IsUnique();

            entity.HasIndex(e => e.Studentid, "studentaccountinfo_studentid_key").IsUnique();

            entity.Property(e => e.Accountid)
                .ValueGeneratedNever()
                .HasColumnName("accountid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Studentid).HasColumnName("studentid");

            entity.HasOne(d => d.Student).WithOne(p => p.Studentaccountinfo)
                .HasForeignKey<Studentaccountinfo>(d => d.Studentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("studentaccountinfo_studentid_fkey");
        });

        modelBuilder.Entity<Studentadditionaldoc>(entity =>
        {
            entity.HasKey(e => e.Documentid).HasName("studentadditionaldocs_pkey");

            entity.ToTable("studentadditionaldocs");

            entity.Property(e => e.Documentid)
                .ValueGeneratedNever()
                .HasColumnName("documentid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Documentpath)
                .HasMaxLength(255)
                .HasColumnName("documentpath");
            entity.Property(e => e.Documenttype)
                .HasMaxLength(100)
                .HasColumnName("documenttype");
            entity.Property(e => e.Studentid).HasColumnName("studentid");

            entity.HasOne(d => d.Student).WithMany(p => p.Studentadditionaldocs)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("studentadditionaldocs_studentid_fkey");
        });

        modelBuilder.Entity<Studentattendance>(entity =>
        {
            entity.HasKey(e => e.Attendanceid).HasName("studentattendance_pkey");

            entity.ToTable("studentattendance");

            entity.Property(e => e.Attendanceid)
                .ValueGeneratedNever()
                .HasColumnName("attendanceid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.Studentid).HasColumnName("studentid");

            entity.HasOne(d => d.Student).WithMany(p => p.Studentattendances)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("studentattendance_studentid_fkey");
        });

        modelBuilder.Entity<Studentdocument>(entity =>
        {
            entity.HasKey(e => e.Documentid).HasName("studentdocuments_pkey");

            entity.ToTable("studentdocuments");

            entity.HasIndex(e => e.Studentid, "studentdocuments_studentid_key").IsUnique();

            entity.Property(e => e.Documentid)
                .ValueGeneratedNever()
                .HasColumnName("documentid");
            entity.Property(e => e.Cnicbackorbform)
                .HasMaxLength(255)
                .HasColumnName("cnicbackorbform");
            entity.Property(e => e.Cnicfrontorbform)
                .HasMaxLength(255)
                .HasColumnName("cnicfrontorbform");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .HasColumnName("photo");
            entity.Property(e => e.Studentid).HasColumnName("studentid");

            entity.HasOne(d => d.Student).WithOne(p => p.Studentdocument)
                .HasForeignKey<Studentdocument>(d => d.Studentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("studentdocuments_studentid_fkey");
        });

        modelBuilder.Entity<Studentfinance>(entity =>
        {
            entity.HasKey(e => e.Financeid).HasName("studentfinances_pkey");

            entity.ToTable("studentfinances");

            entity.Property(e => e.Financeid)
                .ValueGeneratedNever()
                .HasColumnName("financeid");
            entity.Property(e => e.Amountpaid)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("amountpaid");
            entity.Property(e => e.Amountremaining)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("amountremaining");
            entity.Property(e => e.Coursefee)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("coursefee");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Examinationfee)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("examinationfee");
            entity.Property(e => e.Issuedby).HasColumnName("issuedby");
            entity.Property(e => e.Issueddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("issueddate");
            entity.Property(e => e.Month)
                .HasMaxLength(20)
                .HasColumnName("month");
            entity.Property(e => e.Othercharges)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("othercharges");
            entity.Property(e => e.Otherchargesreasons)
                .HasMaxLength(255)
                .HasColumnName("otherchargesreasons");
            entity.Property(e => e.Paiddate).HasColumnName("paiddate");
            entity.Property(e => e.Paidmethod)
                .HasMaxLength(50)
                .HasColumnName("paidmethod");
            entity.Property(e => e.Remarks)
                .HasMaxLength(255)
                .HasColumnName("remarks");
            entity.Property(e => e.Studentid).HasColumnName("studentid");
            entity.Property(e => e.Transportfee)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("transportfee");
            entity.Property(e => e.Tuitionfee)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("tuitionfee");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.IssuedbyNavigation).WithMany(p => p.Studentfinances)
                .HasForeignKey(d => d.Issuedby)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("studentfinances_issuedby_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.Studentfinances)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("studentfinances_studentid_fkey");
        });

        modelBuilder.Entity<Studentsession>(entity =>
        {
            entity.HasKey(e => e.Sessionid).HasName("studentsessions_pkey");

            entity.ToTable("studentsessions");

            entity.Property(e => e.Sessionid)
                .ValueGeneratedNever()
                .HasColumnName("sessionid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Expiresat).HasColumnName("expiresat");
            entity.Property(e => e.Refreshtoken)
                .HasMaxLength(255)
                .HasColumnName("refreshtoken");
            entity.Property(e => e.Studentaccountid).HasColumnName("studentaccountid");

            entity.HasOne(d => d.Studentaccount).WithMany(p => p.Studentsessions)
                .HasForeignKey(d => d.Studentaccountid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("studentsessions_studentaccountid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
