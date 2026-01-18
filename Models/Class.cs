using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;
      
public partial class Class
{
    public Guid Classid { get; set; }

    public string Classgrade { get; set; } = null!;

    public string? Classsection { get; set; }

    public Guid? Schoolid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = [];

    public virtual ICollection<Course> Courses { get; set; } = [];

    public virtual ICollection<Exam> Exams { get; set; } = [];

    public virtual School? School { get; set; }

    public virtual ICollection<Student> Students { get; set; } = [];
}
