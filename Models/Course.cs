using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Course
{
    public Guid Courseid { get; set; }

    public string Boardordepartment { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public string CourseName { get; set; } = null!;

    public Guid? Class { get; set; }

    public Guid? Teacher { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual Class? ClassNavigation { get; set; }

    public virtual Employee? TeacherNavigation { get; set; }
}
