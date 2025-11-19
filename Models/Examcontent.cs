using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Examcontent
{
    public int Examcontentid { get; set; }

    public string Contentpath { get; set; } = null!;

    public int? Examid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Exam? Exam { get; set; }
}
