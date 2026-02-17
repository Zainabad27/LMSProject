using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Employee
{
    public Guid Employeeid { get; set; }

    public Guid? Schoolid { get; set; }

    public string? EmployeeName { get; set; }

    public string? Employeedesignation { get; set; }
    public string? Address { get; set; }

    public bool Isactive { get; set; }

    public string? Religion { get; set; }

    public string? Nationality { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Course> Courses { get; set; } = [];


    public virtual ICollection<Employeeadditionaldoc> Employeeadditionaldocs { get; set; } = [];

    public virtual ICollection<Employeeattendance> Employeeattendances { get; set; } = [];

    public virtual Employeedocument? Employeedocuments { get; set; }

    public virtual ICollection<Employeefinance> Employeefinances { get; set; } = [];

    public virtual ICollection<Exam> Exams { get; set; } = [];

    public virtual School? School { get; set; }

    public virtual ICollection<Studentfinance> Studentfinances { get; set; } = [];
}
