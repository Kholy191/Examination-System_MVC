using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Presistence.Repositories
{
    public class GenericRepository<T, TKey>(ApplicationDbContext dbContext) : IGenericRepository<T,TKey>  where T : BaseEntity<TKey>
    {
        #region Normal Methods
        public async Task AddAsync(T entity)
        {
            var addedEntity = await dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(TKey id)
        {
            var entity = dbContext.Set<T>().Find(id);
            if (entity != null)
            {
                dbContext.Set<T>().Remove(entity);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            var entity = await dbContext.Set<T>().FirstOrDefaultAsync(e => e.Id.Equals(id));
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} not found.");
            }
            return entity;
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }
        #endregion

        #region Get by Specification
        public async Task<T> GetBySpecificationAsync(TKey id, ISpecification<T, TKey> specification)
        {
            var Query = dbContext.Set<T>().AsQueryable();
            Query = SpecificationEvaluation.SpecificationEvaluate(Query,specification);
            return await Query.FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<T>> GetAllBySpecificationAsync(ISpecification<T, TKey> specification)
        {
            var Query = dbContext.Set<T>().AsQueryable();
            Query = SpecificationEvaluation.SpecificationEvaluate(Query, specification);
            return await Query.ToListAsync();
        }
        #endregion
    }
}
