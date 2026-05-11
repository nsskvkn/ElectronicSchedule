namespace Schedule.DAL.Entities;

public class Teacher
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Patronymic { get; set; }
    public string Email { get; set; } = string.Empty;

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public int? UserId { get; set; }
    public User? User { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}