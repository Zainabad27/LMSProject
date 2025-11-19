using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Studentadditionaldoc
{
    public int Documentid { get; set; }

    public int? Studentid { get; set; }

    public string Documentpath { get; set; } = null!;

    public string Documenttype { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public virtual Student? Student { get; set; }
}
