namespace Schedule.DAL.Entities;

public class Lesson
{
    public int Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public int LessonNumber { get; set; }
    public bool IsOddWeek { get; set; }

    public int DisciplineId { get; set; }
    public Discipline Discipline { get; set; } = null!;

    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;

    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public int ClassroomId { get; set; }
    public Classroom Classroom { get; set; } = null!;
}