using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Studentdocument
{
    public Guid Documentid { get; set; }

    public Guid? Studentid { get; set; }

    public string Cnicfrontorbform { get; set; } = null!;

    public string Cnicbackorbform { get; set; } = null!;

    public string Photo { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public virtual Student? Student { get; set; }
}
