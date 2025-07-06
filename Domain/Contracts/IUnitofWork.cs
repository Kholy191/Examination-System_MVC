using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IUnitofWork
    {
        public IGenericRepository<TEntity , K> GetRepository<TEntity,K>() where TEntity : BaseEntity<K>;
        Task SaveChangesAsync();
    }
}
