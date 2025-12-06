using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Studentaccountinfo
{
    public Guid Accountid { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Guid? Studentid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Student? Student { get; set; }

    public virtual ICollection<Studentsession> Studentsessions { get; set; } = new List<Studentsession>();
}
