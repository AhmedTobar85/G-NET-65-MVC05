using GymProject.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.DataAccess.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();

        Task<int> SaveChangesAsync(CancellationToken ct);

    }
}
