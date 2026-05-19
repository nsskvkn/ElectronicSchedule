using Schedule.DAL.Entities;

namespace Schedule.DAL.Repositories.Interfaces;

public interface ITeacherRepository : IRepository<Teacher>
{
    Task<IEnumerable<Teacher>> GetByDepartmentAsync(int departmentId);
    Task<IEnumerable<Discipline>> GetDisciplinesByTeacherAsync(int teacherId);
    Task<IEnumerable<Teacher>> GetByDisciplineAsync(int disciplineId);
}
