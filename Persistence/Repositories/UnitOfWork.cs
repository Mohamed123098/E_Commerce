using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Persentation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            string typeName = typeof(TEntity).Name;
            if (repositories.TryGetValue(typeName, out object value))
                return (IGenericRepository<TEntity,TKey>)value;
            else
            {
                var Repo = new GenericRepository<TEntity, TKey>(_dbContext);
                repositories.Add(typeName,Repo);
                return Repo;


            }    

        }
        

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
        
    }
}
