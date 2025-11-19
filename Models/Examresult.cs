using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Examresult
{
    public int Resultid { get; set; }

    public decimal? Marksscored { get; set; }

    public int? Enrollmentid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Examenrollment? Enrollment { get; set; }
}
