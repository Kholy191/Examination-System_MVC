using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public abstract class Specifications<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region Criteria
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }
        protected Specifications(Expression<Func<TEntity, bool>> _criteria)
        {
            Criteria = _criteria;
        }
        #endregion

        #region OrderBy and OrderByDescending
        public Expression<Func<TEntity, bool>> OrderBy { get; private set; }

        public Expression<Func<TEntity, bool>> OrderByDescending { get; private set; }
        public void SetOrderBy(Expression<Func<TEntity, bool>> orderBy)
        {
            OrderBy = orderBy;
        }
        public void SetOrderByDescending(Expression<Func<TEntity, bool>> orderByDescending)
        {
            OrderByDescending = orderByDescending;
        }
        #endregion

        #region Includes
        public List<Expression<Func<TEntity, object>>> Includes { get; } = [];
        public void AddInclude(Expression<Func<TEntity, object>> include)
        {
            Includes.Add(include);
        }
        #endregion
    }
}
