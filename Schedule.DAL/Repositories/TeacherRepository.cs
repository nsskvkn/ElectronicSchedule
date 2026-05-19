using Microsoft.EntityFrameworkCore;
using Schedule.DAL.Context;
using Schedule.DAL.Entities;
using Schedule.DAL.Repositories.Interfaces;

namespace Schedule.DAL.Repositories;

public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
{
    public TeacherRepository(ScheduleContext context) : base(context) { }

    public async Task<IEnumerable<Teacher>> GetByDepartmentAsync(int departmentId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(t => t.DepartmentId == departmentId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Discipline>> GetDisciplinesByTeacherAsync(int teacherId)
    {
        return await _context.Set<Lesson>()
            .AsNoTracking()
            .Where(l => l.TeacherId == teacherId)
            .Select(l => l.Discipline)
            .Distinct()
            .ToListAsync();
    }

    public async Task<IEnumerable<Teacher>> GetByDisciplineAsync(int disciplineId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(t => t.Lessons.Any(l => l.DisciplineId == disciplineId))
            .ToListAsync();
    }
}
