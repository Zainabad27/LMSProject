using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Examenrollment
{
    public int Enrollmentid { get; set; }

    public int? Examid { get; set; }

    public int? Studentid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Exam? Exam { get; set; }

    public virtual Examresult? Examresult { get; set; }

    public virtual Student? Student { get; set; }
}
