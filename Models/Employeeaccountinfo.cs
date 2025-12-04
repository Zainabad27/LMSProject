using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Employeeaccountinfo
{
    public Guid Accountid { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Guid? Employeeid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Employeesession> Employeesessions { get; set; } = new List<Employeesession>();
}
