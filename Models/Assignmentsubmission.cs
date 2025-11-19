using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Assignmentsubmission
{
    public int Assignmentsubmissionid { get; set; }

    public string Contentpath { get; set; } = null!;

    public decimal? Marksscored { get; set; }

    public int? Studentid { get; set; }

    public int? Assignmentid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Assignment? Assignment { get; set; }

    public virtual Student? Student { get; set; }
}
