namespace Schedule.DAL.Entities;

public class Discipline
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}