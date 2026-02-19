using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity:BaseEntity<Tkey>
    {
        Task AddAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(Tkey id);
        #region With specifications
        Task<List<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications);

        Task<TEntity> GetByIdAsync(ISpecifications<TEntity, Tkey> specifications); 
        #endregion
        void Delete(TEntity entity);
        void Update(TEntity entity);
  
    }
}
