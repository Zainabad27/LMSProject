using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Class
{
    public int Classid { get; set; }

    public string Classgrade { get; set; } = null!;

    public string? Classsection { get; set; }

    public int? Schoolid { get; set; }

    public int? Courseid { get; set; }

    public DateTime? Createdat { get; set; }

    public int? Teacher { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual Course? Course { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual School? School { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual Employee? TeacherNavigation { get; set; }
}
