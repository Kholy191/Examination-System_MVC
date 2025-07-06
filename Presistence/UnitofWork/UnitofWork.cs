using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Presistence.Repositories;

namespace Presistence.UnitofWork
{
    public class UnitofWork : IUnitofWork
    {
        readonly ApplicationDbContext _dbContext;
        public UnitofWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        public IGenericRepository<TEntity, K> GetRepository<TEntity, K>() where TEntity : BaseEntity<K>
        {
            var type = typeof(TEntity);
            if (_repositories.ContainsKey(type))
            {
                return (IGenericRepository<TEntity, K>)_repositories[type];
            }
            else
            {
                var repo = new GenericRepository<TEntity, K>(_dbContext);
                _repositories.Add(type, repo);
                return repo;
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving changes to the database.", ex);
            }
        }
    }
}
