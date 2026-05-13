using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Schedule.DAL.Entities;

namespace Schedule.DAL.Repositories.Interfaces;

public interface ITeacherRepository : IRepository<Teacher>
{
    Task<IEnumerable<Teacher>> GetByDepartmentAsync(int departmentId);
    Task<Teacher?> GetWithDisciplinesAsync(int teacherId);
    Task<IEnumerable<Teacher>> GetByDisciplineAsync(int disciplineId);
}
