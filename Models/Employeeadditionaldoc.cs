using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Employeeadditionaldoc
{
    public int Documentid { get; set; }

    public int? Employeeid { get; set; }

    public string Documentpath { get; set; } = null!;

    public string? Documenttype { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Employee? Employee { get; set; }
}
