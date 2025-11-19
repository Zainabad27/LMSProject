using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Employeefinance
{
    public int Financeid { get; set; }

    public int? Employeeid { get; set; }

    public string Month { get; set; } = null!;

    public decimal Basicsalary { get; set; }

    public decimal? Allowances { get; set; }

    public decimal? Overtimepay { get; set; }

    public decimal? Bonuses { get; set; }

    public decimal? Taxdeduction { get; set; }

    public decimal? Dueonemployee { get; set; }

    public decimal? Otherdeductions { get; set; }

    public string? Otherdeductionsreasons { get; set; }

    public DateTime? Paiddate { get; set; }

    public string? Paidmethod { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Employee? Employee { get; set; }
}
