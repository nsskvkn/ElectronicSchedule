namespace Schedule.BLL.DTO;

public class FilterDTO
{
    public int? GroupId { get; set; }
    public int? TeacherId { get; set; }
    public int? ClassroomId { get; set; }
    public int? DisciplineId { get; set; }
    public DayOfWeek? DayOfWeek { get; set; }
    public bool? IsOddWeek { get; set; }
}