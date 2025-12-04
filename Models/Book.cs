using System;
using System.Collections.Generic;

namespace LmsApp2.Api.Models;

public partial class Book
{
    public Guid Bookid { get; set; }

    public string BookTitle { get; set; } = null!;

    public string? BookAuthor { get; set; }

    public string BookIsbn { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public Guid? Courseid { get; set; }

    public virtual Course? Course { get; set; }
}
