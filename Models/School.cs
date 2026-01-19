
namespace LmsApp2.Api.Models;

public partial class School
{
    public Guid Schoolid { get; set; }

    public string Schoolname { get; set; } = null!;

    public string? Address { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = [];

    public virtual ICollection<Employee> Employees { get; set; } = [];

    public virtual ICollection<Student> Students { get; set; } = [];
}
