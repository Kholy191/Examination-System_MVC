using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Presistence
{
    static public class SpecificationEvaluation
    {
        public static IQueryable<T> SpecificationEvaluate<T, K>(this IQueryable<T> query, ISpecification<T, K> specification) where T : Domain.Entities.BaseEntity<K>
        {

            if (query == null)
            {
                throw new ArgumentNullException(nameof(query), "Query cannot be null.");
            }

            if (specification is null)
            {
                return query;
            }

            var evaluatedQuery = query;

            if (specification.Criteria != null)
            {
                evaluatedQuery = query.Where(specification.Criteria);
            }

            if (specification.OrderBy != null)
            {
                evaluatedQuery = evaluatedQuery.OrderBy(specification.OrderBy).AsQueryable();
            }
            else if (specification.OrderByDescending != null)
            {
                evaluatedQuery = evaluatedQuery.OrderByDescending(specification.OrderByDescending).AsQueryable();
            }

            if (specification.Includes.Count > 0)
            {
                foreach (var item in specification.Includes)
                {
                    evaluatedQuery = evaluatedQuery.Include(item);
                }
            }
            return evaluatedQuery;
        }

    }
}
