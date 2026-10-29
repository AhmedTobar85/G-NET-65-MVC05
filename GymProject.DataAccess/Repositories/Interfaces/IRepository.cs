using GymProject.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GymProject.DataAccess.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false, CancellationToken cancellationToken = default);

        Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = false, CancellationToken cancellationToken = default);
    }
}