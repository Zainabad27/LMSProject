using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Assignment
{
    public Guid Assignmentid { get; set; }

    public Guid? Employeeid { get; set; }

    public Guid? Classid { get; set; }

    public DateTime Deadline { get; set; }

    public decimal? Totalmarks { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Assignmentquestion? Assignmentquestion { get; set; }

    public virtual ICollection<Assignmentsubmission> Assignmentsubmissions { get; set; } = new List<Assignmentsubmission>();

    public virtual Class? Class { get; set; }

    public virtual Employee? Employee { get; set; }
}
