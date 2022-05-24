using HM.DataAccess.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.DataAccess
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        #region Fields
        internal HmDbContext _context;
        internal DbSet<T> _dbSet;
        #endregion

        #region Constructor
        protected RepositoryBase(HmDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        #endregion

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public T? GetById(object id) => _dbSet.Find(id);

        public T? GetFirstOrDefault(Func<T, bool> predicate) => _dbSet.FirstOrDefault<T>(predicate);

        public IEnumerable<T> Get(Func<T, bool> predicate) => _dbSet.Where(predicate).ToList();

        public int Count() => _dbSet.Count();

        public async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            _dbSet.Attach(entity);

        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);
        }

    }
}
