using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Assignmentquestion
{
    public Guid Assignmentquestionid { get; set; }

    public string Content { get; set; } = null!;

    public Guid? Employeeid { get; set; }

    public Guid? Assignmentid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Assignment? Assignment { get; set; }

    public virtual Employee? Employee { get; set; }
}
