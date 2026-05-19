namespace Schedule.BLL.DTO;

public class CreateLessonDTO
{
    public DayOfWeek DayOfWeek { get; set; }
    public int LessonNumber { get; set; }
    public bool IsOddWeek { get; set; }

    public int DisciplineId { get; set; }
    public int TeacherId { get; set; }
    public int GroupId { get; set; }
    public int ClassroomId { get; set; }
}