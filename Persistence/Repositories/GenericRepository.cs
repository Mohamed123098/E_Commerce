using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Persentation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, Tkey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public async Task AddAsync(TEntity entity) =>await _dbContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);


        public async Task<List<TEntity>> GetAllAsync() => await _dbContext.Set<TEntity>().ToListAsync();


        public async Task<TEntity> GetByIdAsync(Tkey id) => await _dbContext.Set<TEntity>().FindAsync(id);


        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);
        public async Task<TEntity> GetByIdAsync(ISpecifications<TEntity, Tkey> specifications)
        => await SpecificationEvalutor<TEntity, Tkey>.CreateQuery(_dbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        public async Task<List<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications)
        =>await SpecificationEvalutor<TEntity, Tkey>.CreateQuery(_dbContext.Set<TEntity>(), specifications).ToListAsync();
    }
}
