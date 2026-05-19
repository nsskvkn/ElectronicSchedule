using Schedule.DAL.Entities;
namespace Schedule.DAL.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ILessonRepository Lessons { get; }
    ITeacherRepository Teachers { get; }
    IRepository<Group> Groups { get; }
    IRepository<Classroom> Classrooms { get; }
    IRepository<Discipline> Disciplines { get; }
    IRepository<Department> Departments { get; }
    IRepository<User> Users { get; }

    Task<int> SaveAsync();
}
