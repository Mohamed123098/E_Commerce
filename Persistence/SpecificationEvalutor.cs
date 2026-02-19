using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class SpecificationEvalutor<TEntity, TKey> where TEntity:BaseEntity<TKey>
    {
        public static IQueryable<TEntity> CreateQuery(IQueryable<TEntity> dbcontext, ISpecifications<TEntity,TKey> specifications)
        {
            var query = dbcontext;
            if(specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            if (specifications.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDescending);
            }


            if (specifications.Criteria is not null)
            {
               query = query.Where(specifications.Criteria);
            }
            if(specifications.IncludeExpressions !=null&& specifications.IncludeExpressions.Count>0)
            {
                foreach (var item in specifications.IncludeExpressions)
                {
                    query = query.Include(item);

                }
            }    
            if(specifications.IsPaginated)
            {
                
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }
            return query;
        }
    }
}
