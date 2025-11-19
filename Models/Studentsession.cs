using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Studentsession
{
    public int Sessionid { get; set; }

    public int? Studentaccountid { get; set; }

    public string Ipaddress { get; set; } = null!;

    public string Refreshtoken { get; set; } = null!;

    public DateTime Expiresat { get; set; }

    public string? Device { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Studentaccountinfo? Studentaccount { get; set; }
}
