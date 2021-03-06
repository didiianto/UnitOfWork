using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace UnitOfWork.Models
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        protected readonly ApplicationDBContext context;

        public Repository(ApplicationDBContext _context)
        {
            this.context = _context;
        }
        public async Task<T> Get(Guid id)
            => await context.Set<T>().FindAsync(id);

        public async Task<T> Find(Expression<Func<T, bool>> predicate)
            => await context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<T>> GetAll()
            => await context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
            => await context.Set<T>().Where(predicate).ToListAsync();

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);    
        }

        public void AddRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }
        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            context.Set<T>().UpdateRange(entities);
        }
        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }

    }
}
