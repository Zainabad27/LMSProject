using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Course
{
    public Guid Courseid { get; set; }

    public string Boardordepartment { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
