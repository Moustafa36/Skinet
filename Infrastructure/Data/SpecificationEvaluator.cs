using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery ( IQueryable<TEntity> inputQuery,ISpecification<TEntity> spec)
        {
            var query = inputQuery ; 
            if (spec.Criteria!=null)
            {
                query = query.Where(spec.Criteria) ;
            }
            if(spec.OrderBy!=null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if(spec.IsPagingEnabled)
            {
                query =query.Skip(spec.Skip).Take(spec.Take);
            }
            query = spec.Includes.Aggregate(query, (Current, Include) => Current.Include(Include) );
            return query ; 
        }
    }
}