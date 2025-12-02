using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Infrastructure.Services
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly OrgFlowDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(OrgFlowDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            // FindAsync koristi primarni ključ (pretpostavljamo da je Id : int)
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await this.GetByIdAsync(id);
            if (entity is null)
                throw new KeyNotFoundException($"Entity with id {id} does not exist!");

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
