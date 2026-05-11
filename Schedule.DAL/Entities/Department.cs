namespace Schedule.DAL.Entities;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    public ICollection<Group> Groups { get; set; } = new List<Group>();
}