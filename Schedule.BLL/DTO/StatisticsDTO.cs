namespace Schedule.BLL.DTO;

public class StatisticsDTO
{
    public int TotalLessons { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public Dictionary<string, int> LessonsByDay { get; set; } = new();
    public Dictionary<string, int> LessonsByDiscipline { get; set; } = new();
}