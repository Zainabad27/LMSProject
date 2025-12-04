using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Examresult
{
    public Guid Resultid { get; set; }

    public decimal? Marksscored { get; set; }

    public Guid? Enrollmentid { get; set; }

    public Guid? Studentid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Examenrollment? Enrollment { get; set; }

    public virtual Student? Student { get; set; }
}
