using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface ISpecification <TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; }
        public Expression<Func<TEntity, bool>> OrderBy { get; }
        public Expression<Func<TEntity, bool>> OrderByDescending { get; }
        public List<Expression<Func<TEntity, object>>> Includes { get; }
    }
}
