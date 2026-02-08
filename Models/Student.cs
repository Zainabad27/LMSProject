using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Student
{
    public Guid Studentid { get; set; }

    public string? StudentName { get; set; }
    public string? Guardianname { get; set; }

    public string? Guardiancontact { get; set; }

    public string? Address { get; set; }

    public string? Gender { get; set; }

    public string? Bloodgroup { get; set; }

    public bool Isactive { get; set; }

    public string? Religion { get; set; }

    public string? Nationality { get; set; }

    public Guid? Schoolid { get; set; }

    public Guid? Classid { get; set; }

    public DateTime? Createdat { get; set; }

    public DateOnly Birthdate { get; set; }

    public virtual ICollection<Assignmentsubmission> Assignmentsubmissions { get; set; } = new List<Assignmentsubmission>();

    public virtual Class? Class { get; set; }

    public virtual ICollection<Examenrollment> Examenrollments { get; set; } = new List<Examenrollment>();

    public virtual ICollection<Examresult> Examresults { get; set; } = new List<Examresult>();

    public virtual School? School { get; set; }


    public virtual ICollection<Studentadditionaldoc> Studentadditionaldocs { get; set; } = new List<Studentadditionaldoc>();

    public virtual ICollection<Studentattendance> Studentattendances { get; set; } = new List<Studentattendance>();

    public virtual Studentdocument? Studentdocument { get; set; }

    public virtual ICollection<Studentfinance> Studentfinances { get; set; } = new List<Studentfinance>();
}
