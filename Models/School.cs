using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class School
{
    public Guid Schoolid { get; set; }

    public string Schoolname { get; set; } = null!;

    public string? Address { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
