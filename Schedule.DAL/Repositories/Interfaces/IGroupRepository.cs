using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Schedule.DAL.Entities;

namespace Schedule.DAL.Repositories.Interfaces;

public interface IGroupRepository : IRepository<Group>
{
    Task<IEnumerable<Group>> GetByDepartmentAsync(int departmentId);
}