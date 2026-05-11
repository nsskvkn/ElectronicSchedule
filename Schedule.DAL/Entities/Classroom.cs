namespace Schedule.DAL.Entities;

public class Classroom
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Building { get; set; } = string.Empty;
    public int Capacity { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}