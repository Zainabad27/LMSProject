using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Studentsession
{
    public Guid Sessionid { get; set; }

    public Guid? Studentaccountid { get; set; }

    public string Refreshtoken { get; set; } = null!;

    public DateTime Expiresat { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Studentaccountinfo? Studentaccount { get; set; }
}
