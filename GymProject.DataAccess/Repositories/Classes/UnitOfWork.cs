using GymProject.DataAccess.Data.Contexts;
using GymProject.DataAccess.Models;
using GymProject.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.DataAccess.Repositories.Classes
{
    public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var typeName = typeof(TEntity).Name;
            if (_repositories.TryGetValue(typeName, out object? value))
                return (IRepository<TEntity>)value;

            else
            {
                var repo = new Repository<TEntity>(dbContext);
                _repositories[typeName] = repo;
                return repo;
            }

        }

        public async Task<int> SaveChangesAsync(CancellationToken ct)
         => await dbContext.SaveChangesAsync();
    }
}
