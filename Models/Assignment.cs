using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Assignment
{
    public int Assignmentid { get; set; }

    public int? Employeeid { get; set; }

    public int? Classid { get; set; }

    public DateTime Deadline { get; set; }

    public decimal? Totalmarks { get; set; }

    public DateTime? Createdat { get; set; }

    public string Subject { get; set; } = null!;

    public virtual ICollection<Assignmentquestion> Assignmentquestions { get; set; } = new List<Assignmentquestion>();

    public virtual ICollection<Assignmentsubmission> Assignmentsubmissions { get; set; } = new List<Assignmentsubmission>();

    public virtual Class? Class { get; set; }

    public virtual Employee? Employee { get; set; }
}
