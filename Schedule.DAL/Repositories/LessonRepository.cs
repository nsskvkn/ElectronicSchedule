using Microsoft.EntityFrameworkCore;
using Schedule.DAL.Context;
using Schedule.DAL.Entities;
using Schedule.DAL.Repositories.Interfaces;

namespace Schedule.DAL.Repositories;

public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
{
    public LessonRepository(ScheduleContext context) : base(context) { }

    public async Task<IEnumerable<Lesson>> GetByGroupAsync(int groupId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(l => l.GroupId == groupId)
            .Include(l => l.Teacher)
            .Include(l => l.Discipline)
            .Include(l => l.Classroom)
            .ToListAsync();
    }

    public async Task<IEnumerable<Lesson>> GetByTeacherAsync(int teacherId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(l => l.TeacherId == teacherId)
            .Include(l => l.Group)
            .Include(l => l.Discipline)
            .Include(l => l.Classroom)
            .ToListAsync();
    }

    public async Task<IEnumerable<Lesson>> GetByClassroomAsync(int classroomId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(l => l.ClassroomId == classroomId)
            .Include(l => l.Teacher)
            .Include(l => l.Group)
            .Include(l => l.Discipline)
            .ToListAsync();
    }

    public async Task<IEnumerable<Lesson>> GetByDayAsync(DayOfWeek day)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(l => l.DayOfWeek == day)
            .Include(l => l.Teacher)
            .Include(l => l.Group)
            .Include(l => l.Discipline)
            .Include(l => l.Classroom)
            .ToListAsync();
    }
}
