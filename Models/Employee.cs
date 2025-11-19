using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Employee
{
    public int Employeeid { get; set; }

    public int? Schoolid { get; set; }

    public string Employeename { get; set; } = null!;

    public string Employeedesignation { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public string? Address { get; set; }

    public bool? Isactive { get; set; }

    public string? Religion { get; set; }

    public string? Nationality { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Assignmentquestion> Assignmentquestions { get; set; } = new List<Assignmentquestion>();

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual Employeeaccountinfo? Employeeaccountinfo { get; set; }

    public virtual ICollection<Employeeadditionaldoc> Employeeadditionaldocs { get; set; } = new List<Employeeadditionaldoc>();

    public virtual ICollection<Employeeattendance> Employeeattendances { get; set; } = new List<Employeeattendance>();

    public virtual Employeedocument? Employeedocument { get; set; }

    public virtual ICollection<Employeefinance> Employeefinances { get; set; } = new List<Employeefinance>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual School? School { get; set; }

    public virtual ICollection<Studentfinance> Studentfinances { get; set; } = new List<Studentfinance>();
}
