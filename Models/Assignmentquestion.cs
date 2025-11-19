using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Assignmentquestion
{
    public int Assignmentquestionid { get; set; }

    public string Content { get; set; } = null!;

    public int? Employeeid { get; set; }

    public int? Assignmentid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Assignment? Assignment { get; set; }

    public virtual Employee? Employee { get; set; }
}
