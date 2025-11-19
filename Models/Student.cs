using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Student
{
    public int Studentid { get; set; }

    public string Studentname { get; set; } = null!;

    public string Guardianname { get; set; } = null!;

    public string Guardiancontact { get; set; } = null!;

    public string? Address { get; set; }

    public string? Gender { get; set; }

    public string? Bloodgroup { get; set; }

    public bool? Isactive { get; set; }

    public string? Religion { get; set; }

    public string? Nationality { get; set; }

    public int? Age { get; set; }

    public int? Schoolid { get; set; }

    public int? Classid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Assignmentsubmission> Assignmentsubmissions { get; set; } = new List<Assignmentsubmission>();

    public virtual Class? Class { get; set; }

    public virtual ICollection<Examenrollment> Examenrollments { get; set; } = new List<Examenrollment>();

    public virtual School? School { get; set; }

    public virtual Studentaccountinfo? Studentaccountinfo { get; set; }

    public virtual ICollection<Studentadditionaldoc> Studentadditionaldocs { get; set; } = new List<Studentadditionaldoc>();

    public virtual ICollection<Studentattendance> Studentattendances { get; set; } = new List<Studentattendance>();

    public virtual Studentdocument? Studentdocument { get; set; }

    public virtual ICollection<Studentfinance> Studentfinances { get; set; } = new List<Studentfinance>();
}
