using GymProject.DataAccess.Data.Contexts;
using GymProject.DataAccess.Models;
using GymProject.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GymProject.DataAccess.Repositories.Classes
{
    public class Repository<TEntity>(AppDbContext dbContext) : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly DbSet<TEntity> _set = dbContext.Set<TEntity>();
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = tracking ? _set : _set.AsNoTracking();
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await _set.FindAsync(id, cancellationToken);
       public async Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _set.Add(entity);

            return await dbContext.SaveChangesAsync(cancellationToken);

        }

        public async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _set.Remove(entity);
           return await dbContext.SaveChangesAsync(cancellationToken);  
        }



       public async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _set.Update(entity);
            return await dbContext.SaveChangesAsync(cancellationToken);

        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _set.AsNoTracking().AnyAsync(predicate, cancellationToken);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool tracking=false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = tracking ? _set : _set.AsNoTracking();
            return await query.FirstOrDefaultAsync(predicate);
        }
    }
}
