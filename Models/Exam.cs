using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Exam
{
    public Guid Examid { get; set; }

    public DateOnly Examdate { get; set; }

    public decimal? Totalmarks { get; set; }

    public string Subject { get; set; } = null!;

    public string Examtype { get; set; } = null!;

    public Guid? Teacherid { get; set; }

    public Guid? Classid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Class? Class { get; set; }

    public virtual ICollection<Examcontent> Examcontents { get; set; } = new List<Examcontent>();

    public virtual ICollection<Examenrollment> Examenrollments { get; set; } = new List<Examenrollment>();

    public virtual Employee? Teacher { get; set; }
}
