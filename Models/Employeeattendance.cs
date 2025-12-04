using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Employeeattendance
{
    public Guid Attendanceid { get; set; }

    public Guid? Employeeid { get; set; }

    public DateOnly Date { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public virtual Employee? Employee { get; set; }
}
