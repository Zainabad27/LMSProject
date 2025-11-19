using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Studentattendance
{
    public long Attendanceid { get; set; }

    public int? Studentid { get; set; }

    public DateOnly Date { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public virtual Student? Student { get; set; }
}
