using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Employeedocument
{
    public int Documentid { get; set; }

    public int? Employeeid { get; set; }

    public string? Cnicfront { get; set; }

    public string? Cnicback { get; set; }

    public string? Photo { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Employee? Employee { get; set; }
}
