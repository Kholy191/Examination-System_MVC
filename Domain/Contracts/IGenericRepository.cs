using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenericRepository<T, TKey> where T : BaseEntity<TKey>
    {
        public  Task AddAsync(T entity);
        public void Delete(TKey id);
        public Task<T> GetByIdAsync(TKey id) ;
        public Task<IEnumerable<T>> GetAllAsync();
        public void Update(T entity);
        #region Get by Specification
        public Task<T> GetBySpecificationAsync(TKey id, ISpecification<T,TKey> specification);
        public Task<IEnumerable<T>> GetAllBySpecificationAsync(ISpecification<T, TKey> specification);
        #endregion
    }
}
