using Schedule.DAL.Context;
using Schedule.DAL.Entities;
using Schedule.DAL.Repositories.Interfaces;

namespace Schedule.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ScheduleContext _context;

    private ILessonRepository? _lessons;
    private ITeacherRepository? _teachers;
    private IRepository<Group>? _groups;
    private IRepository<Classroom>? _classrooms;
    private IRepository<Discipline>? _disciplines;
    private IRepository<Department>? _departments;
    private IRepository<User>? _users;

    public UnitOfWork(ScheduleContext context)
    {
        _context = context;
    }

    public ILessonRepository Lessons =>
        _lessons ??= new LessonRepository(_context);

    public ITeacherRepository Teachers =>
        _teachers ??= new TeacherRepository(_context);

    public IRepository<Group> Groups =>
        _groups ??= new GenericRepository<Group>(_context);

    public IRepository<Classroom> Classrooms =>
        _classrooms ??= new GenericRepository<Classroom>(_context);

    public IRepository<Discipline> Disciplines =>
        _disciplines ??= new GenericRepository<Discipline>(_context);

    public IRepository<Department> Departments =>
        _departments ??= new GenericRepository<Department>(_context);

    public IRepository<User> Users =>
        _users ??= new GenericRepository<User>(_context);

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
