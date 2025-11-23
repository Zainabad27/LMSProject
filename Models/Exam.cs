using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Exam
{
    public int Examid { get; set; }

    public string Examdate { get; set; } = null!;

    public decimal? Totalmarks { get; set; }

    public string Subject { get; set; } = null!;

    public string Examtype { get; set; } = null!;

    public int? Teacherid { get; set; }

    public int? Classid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Class? Class { get; set; }

    public virtual ICollection<Examcontent> Examcontents { get; set; } = new List<Examcontent>();

    public virtual ICollection<Examenrollment> Examenrollments { get; set; } = new List<Examenrollment>();

    public virtual Employee? Teacher { get; set; }
}
