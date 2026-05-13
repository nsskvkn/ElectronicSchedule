using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.DAL.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ILessonRepository Lessons { get; }
    ITeacherRepository Teachers { get; }
    IGroupRepository Groups { get; }

    Task<int> SaveAsync();
    int Save();
}
