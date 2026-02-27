using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Assignmentsubmission
{ 
    public Guid Assignmentsubmissionid { get; set; }

    public string Content { get; set; } = null!;

    public decimal? Marksscored { get; set; }

    public Guid? Studentid { get; set; }

    public Guid? Assignmentid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Assignment? Assignment { get; set; }

    public virtual Student? Student { get; set; }
}
