namespace Schedule.BLL.DTO;

public class LessonDTO
{
    public int Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public int LessonNumber { get; set; }
    public bool IsOddWeek { get; set; }

    public string DisciplineName { get; set; } = string.Empty;
    public string TeacherFullName { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public string ClassroomNumber { get; set; } = string.Empty;
    public string ClassroomBuilding { get; set; } = string.Empty;
}