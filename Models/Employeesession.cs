using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Employeesession
{
    public int Sessionid { get; set; }

    public int? Employeeaccountid { get; set; }

    public string Refreshtoken { get; set; } = null!;

    public DateTime Expiresat { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Employeeaccountinfo? Employeeaccount { get; set; }
}
