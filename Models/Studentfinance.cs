using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Studentfinance
{
    public Guid Financeid { get; set; }

    public Guid? Studentid { get; set; }

    public Guid? Issuedby { get; set; }

    public decimal? Tuitionfee { get; set; }

    public decimal? Examinationfee { get; set; }

    public decimal? Coursefee { get; set; }

    public decimal? Transportfee { get; set; }

    public decimal? Othercharges { get; set; }

    public string? Otherchargesreasons { get; set; }

    public string Month { get; set; } = null!;

    public DateTime? Issueddate { get; set; }

    public decimal? Amountpaid { get; set; }

    public decimal? Amountremaining { get; set; }

    public string? Paidmethod { get; set; }

    public string? Remarks { get; set; }

    public DateOnly? Paiddate { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Employee? IssuedbyNavigation { get; set; }

    public virtual Student? Student { get; set; }
}
