using Schedule.DAL.Entities;

namespace Schedule.DAL.Repositories.Interfaces;

public interface ILessonRepository : IRepository<Lesson>
{
    Task<IEnumerable<Lesson>> GetByGroupAsync(int groupId);
    Task<IEnumerable<Lesson>> GetByTeacherAsync(int teacherId);
    Task<IEnumerable<Lesson>> GetByClassroomAsync(int classroomId);
    Task<IEnumerable<Lesson>> GetByDayAsync(DayOfWeek day);
}