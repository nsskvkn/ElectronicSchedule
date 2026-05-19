using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schedule.DAL.Context;
using Schedule.DAL.Repositories.Interfaces;

namespace Schedule.DAL.Repositories;

public class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly ScheduleContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ScheduleContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        var entry = _context.Entry(entity);
        if (entry.State == EntityState.Detached)
            _dbSet.Attach(entity);
        entry.State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        var entry = _context.Entry(entity);
        if (entry.State == EntityState.Detached)
        {
            var keyValue = entry.Property("Id").CurrentValue;
            var tracked = _context.ChangeTracker.Entries<T>()
                .FirstOrDefault(e => Equals(e.Property("Id").CurrentValue, keyValue));

            if (tracked != null)
            {
                tracked.State = EntityState.Deleted;
                return;
            }

            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
    }
}